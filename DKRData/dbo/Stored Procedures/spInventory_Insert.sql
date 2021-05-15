﻿CREATE PROCEDURE [dbo].[spInventory_Insert]
	@ProductId INT,
	@Quantity INT,
	@PurchasePrice DECIMAL(19,4),
	@PurchaseDate DATETIME2
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.Inventory(ProductId, Quantity, PurchasePrice, PurchaseDate)
	VALUES (@ProductId, @Quantity, @PurchasePrice, @PurchaseDate);
END