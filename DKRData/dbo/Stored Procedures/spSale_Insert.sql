CREATE PROCEDURE [dbo].[spSale_Insert]
	@Id INT,
	@CashierId NVARCHAR(128),
	@SaleDate DATETIME2,
	@SubTotal DECIMAL(19,4),
	@Tax DECIMAL(19,4),
	@Total DECIMAL(19,4)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Sale(CashierId, SaleDate, SubTotal, Tax, Total)
	VALUES (@CashierId, @SaleDate, @SubTotal, @Tax, @Total);

	SELECT @@IDENTITY;
END