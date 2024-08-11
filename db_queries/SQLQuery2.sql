ALTER PROC SP_DELETEPRODUCT
(
	@PRODUCTID INT,
	@OUTPUTMESSAGE VARCHAR(50) OUTPUT
)
AS
BEGIN

declare @rowcount int = 0

	BEGIN TRY

	set @rowcount = (select count(1) from dbo.table_Product where PRODUCTID = @PRODUCTID)

	if(@rowcount > 0)
		begin
			BEGIN TRAN
					DELETE FROM dbo.table_Product
					WHERE PRODUCTID = @PRODUCTID

					set @OUTPUTMESSAGE = 'Product deleted successfully!'
			COMMIT TRAN
		end
	else
		begin
			set @OUTPUTMESSAGE = 'Product not available with id' + CONVERT(varchar, @PRODUCTID)
		end
	END TRY
BEGIN CATCH
	ROLLBACK TRAN
		SET @OUTPUTMESSAGE = ERROR_MESSAGE()
END CATCH

END