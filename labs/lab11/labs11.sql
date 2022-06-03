drop table Employee;
drop table Clients;
drop table Products;
drop table Orders;
drop table Orders_products;

create table Employee
(id_employee integer primary key not null,
name_employee text  not null,
lastname_employee text not null
);
create table Clients
( id_client integer primary key not null,
phone text,
adress text not null
);

Create table Products
( id_product integer primary key,
product_name text not null,
price real not null
);
Create table Orders
(
id_order integer primary key autoincrement,
order_employee_id  integer, order_client_id integer,all_price real,
 constraint FK_order_employee foreign  key(order_employee_id) references Employee(id_employee),
  constraint FK_order_client foreign  key(order_client_id) references Clients(id_client)
);
create table Orders_products(
order_id int references Orders(id_order),
product_id int references Products(id_product),
product_count int check(product_count>0),
constraint orders_products_pk primary key(order_id, product_id)
);

insert into Employee(name_employee, lastname_employee)
		values('ivan','ivanov');
insert into Employee(name_employee, lastname_employee)
		values('petr','petrov');

insert into Clients (phone,adress)
	values ('+375447372777','doma');
insert into Clients (phone,adress)
	values ('+3752932658975', 'nedoma');

insert into Products(product_name,price)
	values('odin',50);
insert into Products(product_name,price)
	values('dva',20);

insert into Orders(order_employee_id,order_client_id)
	values(1,2);
insert into Orders_products(order_id,product_id, product_count)
	values(2,1,2);
select * from Employee;
select * from Clients;
select * from Products;
select * from Orders;
select * from Orders_products;

create view All_Orders as select * from Orders;

CREATE TRIGGER UpdateAllPrice AFTER INSERT ON Orders_products
BEGIN
UPDATE Orders
SET all_price =all_price+ Products.price * Orders_products.product_count
  from Orders_products inner join Orders on Orders_products.order_id=Orders.id_order
  inner join Products  on Orders_products.product_id=Products.id_product;
END;

drop trigger UpdateAllPrice;
-------------------------CLIENTS-----------------------------------
create procedure newClient
			@phone nvarchar(20),
			@adress nvarchar(20)
as
begin insert into Clients(phone, adress)
	values (@phone,@adress)
	end;
go
exec newClient 'e','eee';
go
create procedure delClient
			@idclient int
			as
			begin delete from Clients where id_client=@idclient
			end;
go
create procedure updatePhone
				@idclient int,
				@phone nvarchar(20)
as
begin
update Clients set phone = @phone where id_client=@idclient;
end;
exec updatePhone 1, '123456789';
go
create procedure getAllClients
	as
	begin select * from Clients
	end;
go

--------------------------------------EMPLOYEE---------------------------------
create procedure addNewEmployee
			@name nvarchar(40),
			@lastname nvarchar(20)
as begin
insert into Employee (name_employee,lastname_empoyee)
	values(@name,@lastname)
end;
go
create procedure getAllEmployee
as begin
select * from Employee;
end;
go
create procedure deleteEmployee
			@id int
as begin
delete from Employee where id_employee=@id
end;
go
----------------------------------Products---------------------------------------
create procedure getAllProducts
as begin
select * from Products;
end;
go
--------------------------------ORDERS--------------------------------------
go
create procedure newOrder
					@order_employee_id int,
					@order_client_id int
as	begin
declare @order_date datetime;
set @order_date = GETDATE();
	insert into Orders(order_employee_id,order_client_id,order_date,all_price)
		values(@order_employee_id,@order_client_id,@order_date,0)
	end;
go
create procedure addProductinOrder
				@order_id int,
				@product_id int,
				@product_count int
as begin
insert into Orders_products(order_id,product_id, product_count)
	values(@order_id,@product_id,@product_count)
end;
go
create procedure getAllOrders
as begin
select * from Orders
end;
go
-----------------------------------TASK-----------------------------------------
create procedure spisok_orders
			@datestart datetime,
			@dateend datetime
as begin
select * from Orders where order_date between @datestart and @dateend;
end;
select * from Orders;
exec spisok_orders '10-10-2000','10-10-2022';
----------------------------------------LAB3-----------------------------------------
exec sp_configure 'clr enabled', 1;
reconfigure;
exec sp_configure 'show advanced options', 1;
reconfigure;
exec sp_configure 'clr strict security', 0;
reconfigure;

drop procedure GetOrdersBetweenDate;
drop function ReadTextFile;
drop type zachem;
drop assembly SQLCLRDemo;

create assembly SQLCLRDemo from 'D:\GitHub\6_Sem_DB\labs\lab3\bin\Debug\lab3.dll'  with permission_set = external_access;
go
create function  ReadTextFile (@path nvarchar(256),
                                @pathto nvarchar(256))
    RETURNS bit WITH EXECUTE AS CALLER
as external name SQLCLRDemo.StoredProcedures.ReadTextFile;

go
exec ReadTextFile @path = N'D:\GitHub\6_Sem_DB\labs\lab3\text.txt', @pathto = N'D:\GitHub\6_Sem_DB\labs\lab3\to.txt';
go

create procedure GetOrdersBetweenDate @beginning datetime, @end datetime
as
    external name SQLCLRDemo.StoredProcedures.GetOrdersBetweenDate;
go
exec GetOrdersBetweenDate @beginning ='10-10-2003', @end = '10-10-2022';
go
create type zachem
external name SQLCLRDemo.UserData;
go
declare @s zachem
set @s = '375445555555, dom'
select @s.ToString();
go

-------------------------------------
exec sp_configure 'clr enabled', 1;
go
reconfigure;
go
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
go

EXEC sp_configure 'clr strict security', 1;
RECONFIGURE;
go
exec sp_changedbowner 'sa'
ALTER DATABASE DB6SEM1_3 SET TRUSTWORTHY ON

SELECT * FROM sys.configurations WHERE name LIKE 'clr strict security';

create assembly Hotels_Assembly from 'D:\Labs\DB\Lab3\obj\Debug\Lab3.dll'
with permission_set = external_access;
