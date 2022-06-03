use Shop_2;
select * from orders
select	distinct(dilers.id_diler), dilers.name_diler as Дилер, 
			SUM(orders.summ) OVER (order by name_diler asc)
			as Число_продуктов from orders
			join dilers ON dilers.id_diler = orders.id_diler;

		
--сравн с общим объемом заказов (%)
		with Aggregates as
		(
			select distinct(id_diler), sum(summ) as sumval
			from orders
			group by orders.id_diler
		)
		select distinct (D.name_diler), O.summ,
			cast (100. * O.summ / A.sumval as numeric(5,2)) as pctcust
			from orders as O
			join aggregates as A on O.id_diler = A.id_diler
			join dilers as D on D.id_diler = O.id_diler;



--сравн с наилучшим объемом заказов
		select dilers.name_diler as Имя_поставщика
			, orders.summ as Заработок
			, max(summ) over (partition by orders.id_diler) as Макс_заработок
			, cast(100. * summ / max(summ) over (partition by orders.id_diler)
					as decimal(5,2)) as Процент
			from orders
			join dilers on dilers.id_diler = orders.id_diler;

			--3.разбиение результатов запроса на страницы.
WITH CTE AS
(
  SELECT  orders.*, 
     ROW_NUMBER() OVER ( ORDER BY id_client ) AS RowNum
  FROM orders
  WHERE summ>5
)
SELECT * FROM CTE 
WHERE RowNum >= 1 AND RowNum < 21
--------------------------
declare @page int = 1;
with RowsPersonal AS
(
    SELECT id_diler, id_client, summ,
    ROW_NUMBER() OVER (ORDER BY id_order) AS RowNumber
    FROM orders
)
select id_diler, id_client, RowNumber
FROM RowsPersonal
WHERE RowNumber BETWEEN 20 * @page AND 20 * (@page + 1);
-------------------------
SELECT * FROM orders ORDER BY id_order OFFSET 0 ROWS FETCH NEXT 20 ROWS ONLY;
--4. ROW_NUMBER() для удаления дубликатов (в partition надо все поля перечислить)

	select count(*) from orders;
	select * from orders;
	insert into orders values (2,3,1,12, '1-01-2000'), (2,3,1,12, '1-01-2000'), (2,3,1,12, '1-01-2000');
	delete x from (
	  select *, rn=row_number() over (partition by id_diler, id_client, id_product, summ, order_date, order_year order by id_diler)
	  from orders
	) x
	where rn > 1;

	
select * from orders;
select * from dilers;
select * from clients;

--------------------------------5(сумма последнего заказа для клиентов(боже как сложно аааааааааа)--------------------
SELECT summ
FROM orders o,
(SELECT id_client, MAX(order_date) AS 'Date'
FROM orders
GROUP BY id_client) AS r
WHERE o.id_client = r.id_client
AND o.order_date = r.Date

---------------------------------6(а нет, вот это сложно)--------------------
select id_client, id_diler, (select count(*) from orders where ) as 'kol-vo' from orders;

select id_client, id_diler,id_product,  count(id_product) 
over(partition by id_product order by id_product)
as maxOrders from orders 
order by maxOrders desc;