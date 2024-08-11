ALTER PROCEDURE sp_InsertProducts
(
@ProductName nvarchar(50),
@Price decimal(8,2),
@Quantity int,
@Remarks nvarchar(50) = NULL
)
as
begin
declare @RowCount int = 0

set @RowCount = (select count(1) from dbo.table_Product where ProductName = @ProductName)

	begin try
		begin tran

		if(@RowCount = 0)
			begin
				insert into dbo.table_Product(ProductName, Price, Quantity, Remarks)
				values(@ProductName, @Price, @Quantity, @Remarks)
			end
		commit tran
	end try
begin catch
	rollback tran
	select ERROR_MESSAGE()
end catch
end