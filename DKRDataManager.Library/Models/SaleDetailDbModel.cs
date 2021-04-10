namespace DKRDataManager.Library.Models
{
    public class SaleDetailDbModel
    {
        public int ProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Quantity { get; set; }
        public int SaleId { get; set; }
        public decimal Tax { get; set; }
    }
}