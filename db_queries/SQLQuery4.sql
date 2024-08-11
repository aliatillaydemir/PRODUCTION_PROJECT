alter proc sp_GetProductById
(
@ProductID int
)
as
begin
		select PRODUCTID
			,[ProductName]
			,[Price]
			,[Quantity]
			,[Remarks]
		FROM [ADO_EXAMPLE].[dbo].[table_Product]
		where PRODUCTID = @ProductID
end