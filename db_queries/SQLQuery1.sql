ALTER PROCEDURE sp_GetAllProducts
as
begin

	select ProductID, ProductName, Price, Quantity, Remarks from dbo.table_Product with(nolock)

end