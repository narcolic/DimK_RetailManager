using System.ComponentModel;

namespace DKRDesktopUI.Models
{
    public class CartItemDisplayModel : INotifyPropertyChanged
    {
        private int _quantityInCart;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DisplayText => $"{Product.ProductName} ({QuantityInCart})";
        public ProductDisplayModel Product { get; set; }

        public int QuantityInCart
        {
            get { return _quantityInCart; }
            set
            {
                _quantityInCart = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuantityInCart)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayText)));
            }
        }
    }
}