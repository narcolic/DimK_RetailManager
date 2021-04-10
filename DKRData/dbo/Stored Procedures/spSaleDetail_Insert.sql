CREATE PROCEDURE [dbo].[spSaleDetail_Insert]
	@SaleId INT,
	@ProductId INT,
	@Quantity INT,
	@PurchasePrice DECIMAL(19,4),
	@Tax DECIMAL(19,4)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.SaleDetail(SaleId, ProductId, Quantity, PurchasePrice, Tax)
	VALUES (@SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax);

END