using System;

namespace DKRDataManager.Library.Models
{
    public class SaleDbModel
    {
        public string CashierId { get; set; }
        public int Id { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}