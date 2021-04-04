using Caliburn.Micro;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Helpers;
using DKRDesktopUI.Library.Models;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DKRDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IConfigHelper _configHelper;
        private readonly IProductEndpoint _productEndpoint;
        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
        private int _itemQuantity = 1;
        private BindingList<ProductModel> _products;

        private ProductModel _selectedProduct;

        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
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

        public string SubTotal => CalculateSubTotal().ToString("C");

        public string Tax => CalculateTax().ToString("C");

        public string Total => (CalculateSubTotal() + CalculateTax()).ToString("C");

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
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        public void CheckOut()
        {
        }

        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProductsAsync();
        }

        private decimal CalculateSubTotal() => Cart.Sum(i => i.Product.RetailPrice * i.QuantityInCart);

        private decimal CalculateTax() => Cart.Sum(i => i.Product.RetailPrice * i.QuantityInCart * (i.Product.IsTaxable ? _configHelper.GetTaxRate() : 0));

        private async Task LoadProductsAsync() => Products = new BindingList<ProductModel>(await _productEndpoint.GetAllAsync());
    }
}