use DBLab6;
drop table Employee;
drop table Clients;
drop table Products;
drop table Orders;
drop table Orders_products;

create table Employee
(id_employee int primary key not null,
name_employee nvarchar(40) UNIQUE  not null,
lastname_employee nvarchar(20) not null,
)
create table Clients
( id_client int primary key not null,
phone nvarchar(20),
adress nvarchar(20) not null,
);

Create table Products
( id_product int primary key,
product_name nvarchar(20) not null,
price float not null,
);
Create table Orders
(
id_order int identity(1,1) primary key,
order_employee_id  int constraint FK_order_employee foreign  key references Employee(id_employee),
order_client_id int  constraint FK_order_client foreign  key references Clients(id_client),
order_date datetime,
all_price float
);
create table Orders_products(
order_id int references Orders(id_order),
product_id int references Products(id_product),
product_count int check(product_count>0),
constraint orders_products_pk primary key(order_id, product_id)
)
drop table Report;
drop procedure GenerateXml;
create table Report(
	id int primary key identity,
	xml_column xml
);
go
create  procedure GenerateXml 
	@result xml output
as
begin
 select @result = (select id_order, name_employee, order_employee_id, order_client_id, order_date from Orders inner join 
Clients on Clients.id_client=Orders.order_client_id inner join Employee
on Employee.id_employee=Orders.order_employee_id
						for xml path, root('rows'));
end;
go
drop procedure InsertXmlIntoReport;
go
create procedure InsertXmlIntoReport 
	@value xml 
as
begin
  insert into Report(xml_column) values (@value);
end;

declare @xml xml;
exec GenerateXml @xml output;
select @xml;
exec InsertXmlIntoReport @xml;
select * from Report;

create primary xml index index_xml_column ON Report(xml_column);

drop procedure ExtractElementFromXml;
go;

create procedure ExtractElementFromXml @id_order int, @row_id int as
begin
	declare  @xml xml, @order int, @name_employee nvarchar(40), @order_employee_id int, 
			 @order_client_id int, @order_date datetime;


	select @xml = (select xml_column.query('rows/row[id_order= sql:variable("@id_order")]') from Report where id = @row_id);

	set @order						= @xml.value('row[1]/id_order[1]', 'int');
	set @name_employee				= @xml.value('row[1]/name_employee[1]', 'nvarchar(40)');
	set @order_employee_id			= @xml.value('row[1]/order_employee_id[1]', 'int');
	set @order_client_id			= @xml.value('row[1]/order_client_id[1]', 'int');
	set @order_date					= @xml.value('row[1]/order_date[1]', 'datetime');

	select @order as OrderId, @name_employee as NameEmlpoyee, @order_employee_id as EmployeeId,
	@order_client_id as IdClient, @order_date as OrderDate;
end;

go

select * from Report;

exec ExtractElementFromXml 2, 1;
