using System.ComponentModel;

namespace DKRDesktopUI.Models
{
    public class ProductDisplayModel : INotifyPropertyChanged
    {
        private int _quantityInStock;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsTaxable { get; set; }
        public string ProductName { get; set; }

        public int QuantityInStock
        {
            get { return _quantityInStock; }
            set
            {
                _quantityInStock = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuantityInStock)));
            }
        }

        public decimal RetailPrice { get; set; }
    }
}