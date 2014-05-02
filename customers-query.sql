DECLARE @Name varchar(100)

SET @Name =  + 'test-200'

select count( *)  from Customers where Customers.Name LIKE ('CustomerName-' + @Name +'%')

select top 1 * from Customers where Customers.Name LIKE 'CustomerName-' + @name+ '%' order by Customers.CreatedOn Desc

select top 1 *  from Customers where Customers.Name LIKE 'CustomerName-' + @name+ '%' order by Customers.CreatedOn 

/*
select *, DATEDIFF(millisecond, '2014-05-02 08:14:28.407',Customers.CreatedOn)  from Customers where Customers.Name LIKE 'CustomerName-test13%'  order by Customers.CreatedOn 


select  Customers.CreatedOn from Customers where Customers.Name LIKE 'CustomerName-test13%' order by Customers.CreatedOn 

*/