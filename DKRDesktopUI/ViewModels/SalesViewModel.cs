using Caliburn.Micro;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DKRDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductEndpoint _productEndpoint;
        private BindingList<ProductModel> _cart;
        private int _itemQuantity;
        private BindingList<ProductModel> _products;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        public bool CanAddToCart { get; }

        public bool CanCheckOut { get; }

        public bool CanRemoveFromCart { get; }

        public BindingList<ProductModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
            }
        }

        public BindingList<ProductModel> Products
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

        public void CheckOut()
        {
        }

        public void RemoveFromCart()
        {
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync() => Products = new BindingList<ProductModel>(await _productEndpoint.GetAllAsync());
    }
}