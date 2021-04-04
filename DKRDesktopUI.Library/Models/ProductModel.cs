namespace DKRDesktopUI.Library.Models
{
    public class ProductModel
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsTaxable { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public decimal RetailPrice { get; set; }
    }
}