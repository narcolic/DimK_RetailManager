using Caliburn.Micro;
using System.ComponentModel;

namespace DKRDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private BindingList<string> _cart;
        private string _itemQuantity;
        private BindingList<string> _products;

        public bool CanAddToCart { get; }

        public bool CanRemoveFromCart { get; }
        public bool CanCheckOut { get; }

        public BindingList<string> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public string ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public BindingList<string> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public string SubTotal { get; } = "$0.00";
        public string Tax { get; } = "$0.00";
        public string Total { get; } = "$0.00";

        public void AddToCart()
        {
        }

        public void RemoveFromCart()
        {
        }

        public void CheckOut()
        {
        }
    }
}