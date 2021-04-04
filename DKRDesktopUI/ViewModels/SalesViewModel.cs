using Caliburn.Micro;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Models;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DKRDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IProductEndpoint _productEndpoint;
        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
        private int _itemQuantity = 1;
        private BindingList<ProductModel> _products;

        private ProductModel _selectedProduct;

        public SalesViewModel(IProductEndpoint productEndpoint)
        {
            _productEndpoint = productEndpoint;
        }

        public bool CanAddToCart => ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity;

        public bool CanCheckOut { get; }

        public bool CanRemoveFromCart { get; }

        public BindingList<CartItemModel> Cart
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
                NotifyOfPropertyChange(() => CanAddToCart);
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

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }

        public string SubTotal => Cart.Sum(i => i.Product.RetailPrice * i.QuantityInCart).ToString("C");

        public string Tax { get; } = "$0.00";

        public string Total { get; } = "$0.00";

        public void AddToCart()
        {
            var existingItemOnCart = Cart.FirstOrDefault(i => i.Product.Equals(SelectedProduct));
            if (existingItemOnCart != null)
            {
                existingItemOnCart.QuantityInCart += ItemQuantity;
            }
            else
            {
                Cart.Add(new CartItemModel()
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                });
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            Cart.ResetBindings();

            NotifyOfPropertyChange(() => SubTotal);
        }

        public void CheckOut()
        {
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync() => Products = new BindingList<ProductModel>(await _productEndpoint.GetAllAsync());
    }
}