use master;
create database Lab5;
use Lab5;

go 
exec sp_configure 'clr enabled',1;
go
reconfigure;
go
drop table Employee;

create table Employee
(hid hierarchyid not null primary key clustered,
id_employee int not null,
name_employee nvarchar(40) UNIQUE  not null,
lastname_employee nvarchar(20) not null,
)
select * from Employee;
--------------------------------------
insert into Employee(hid, id_employee, name_employee,lastname_employee)
values(hierarchyid::GetRoot(),1,'ivan','grishin');
declare @id hierarchyid;
select @id= max(hid) from Employee where hid.GetAncestor(1) = hierarchyid::GetRoot();
insert into Employee(hid, id_employee, name_employee,lastname_employee)
values(hierarchyid::GetRoot().GetDescendant(@id, null),2,'kirill','kravchenko');
-----------------------------------
go
create or alter procedure gethierarchyById @id hierarchyid
as
begin
select hid.ToString()[string], hid.GetLevel()[level], * from Employee where hid.IsDescendantOf(@id) = 1;--дочерний
end;
--------------------------------------
go
create or alter procedure insertValue
@hid hierarchyid, @id int, @name_employee nvarchar(40), @lastname_employee nvarchar(20)
as
begin
declare @LCV hierarchyid
begin transaction
		select @LCV=max(Hid)
		from Employee where
		Hid.GetAncestor(1)=@HID; --id заданнного узла
		INSERT INTO Employee(hid, id_employee, name_employee, lastname_employee) 
					VALUES (@HID.GetDescendant(@LCV,NULL), @id,
					@name_employee, @lastname_employee);
					commit;
end;
---------------------------------------
go
create or alter procedure MoveOrg  @oldparent hierarchyid,
							@newparent hierarchyid
as begin
DECLARE children_cursor CURSOR FOR  
	SELECT Hid FROM Employee
		WHERE Hid.GetAncestor(1) = @OldParent; 
		
DECLARE @ChildId hierarchyid;  
	OPEN children_cursor  
		FETCH NEXT FROM children_cursor INTO @ChildId;	
		
WHILE @@FETCH_STATUS = 0  
BEGIN  
START:  
    DECLARE @NewId hierarchyid;  
    SELECT @NewId = @NewParent.GetDescendant(MAX(Hid), NULL)  
    FROM Employee WHERE Hid.GetAncestor(1) = @NewParent;  

    UPDATE Employee
    SET Hid = Hid.GetReparentedValue(@ChildId, @NewId)  
		WHERE hid.IsDescendantOf(@ChildId) = 1;  

    IF @@error <> 0 GOTO START  
        FETCH NEXT FROM children_cursor INTO @ChildId;  
END  
CLOSE children_cursor;  
DEALLOCATE children_cursor;  
end;
go
------------------------------
exec gethierarchyById '/';
---------------------
exec insertValue '/2/1/', 20, 'nikita', 'kravcenko';
---------------
exec MoveOrg '/2/1/', '/2/2/';
select * from Employee
