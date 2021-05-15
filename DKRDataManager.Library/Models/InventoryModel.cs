using System;

namespace DKRDataManager.Library.Models
{
    public class InventoryModel
    {
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public int Quantity { get; set; }
    }
}