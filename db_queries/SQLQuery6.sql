alter proc [dbo].[sp_UpdateProducts]
(
@ProductID int,
@ProductName nvarchar(50),
@Price decimal(8,2),
@Quantity int,
@Remarks nvarchar(50) = NULL
)
as
begin
declare @RowCount int = 0

set @RowCount = (select count(1) from dbo.table_Product where ProductName = @ProductName and PRODUCTID <> @ProductID)

	begin try
		begin tran

		if(@RowCount = 0)
			begin
				update dbo.table_Product
					set ProductName = @ProductName,
						Price = @Price,
						Quantity = @Quantity,
						Remarks = @Remarks
				where PRODUCTID = @ProductID


			end
		commit tran
	end try
begin catch
	rollback tran
	select ERROR_MESSAGE()
end catch
end