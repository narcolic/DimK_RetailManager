using AutoMapper;
using Caliburn.Micro;
using DKRDesktopUI.Library.Api;
using DKRDesktopUI.Library.Helpers;
using DKRDesktopUI.Library.Models;
using DKRDesktopUI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DKRDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private readonly IConfigHelper _configHelper;
        private readonly IMapper _mapper;
        private readonly IProductEndpoint _productEndpoint;
        private readonly ISaleEndpoint _saleEndpoint;
        private BindingList<CartItemDisplayModel> _cart = new BindingList<CartItemDisplayModel>();
        private int _itemQuantity = 1;
        private BindingList<ProductDisplayModel> _products;
        private CartItemDisplayModel _selectedCartItem;
        private ProductDisplayModel _selectedProduct;

        public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper, ISaleEndpoint saleEndpoint,
            IMapper mapper)
        {
            _productEndpoint = productEndpoint;
            _configHelper = configHelper;
            _saleEndpoint = saleEndpoint;
            _mapper = mapper;
        }

        public bool CanAddToCart => ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity;

        public bool CanCheckOut => Cart.Count > 0;

        public bool CanRemoveFromCart => SelectedCartItem != null && SelectedCartItem.Product.QuantityInStock > 0;

        public BindingList<CartItemDisplayModel> Cart
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

        public BindingList<ProductDisplayModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public CartItemDisplayModel SelectedCartItem
        {
            get { return _selectedCartItem; }
            set
            {
                _selectedCartItem = value;
                NotifyOfPropertyChange(() => SelectedCartItem);
                NotifyOfPropertyChange(() => CanRemoveFromCart);
            }
        }

        public ProductDisplayModel SelectedProduct
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
                Cart.Add(new CartItemDisplayModel()
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                });
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

        public async Task CheckOutAsync()
        {
            var sale = new SaleModel() { SaleDetails = new List<SaleDetailModel>() };
            sale.SaleDetails.AddRange(Cart.Select(item => new SaleDetailModel()
            {
                ProductId = item.Product.Id,
                Quantity = item.QuantityInCart
            }));

            await _saleEndpoint.PostSaleAsync(sale);

            await ResetSalesViewModelASync();
        }

        public void RemoveFromCart()
        {
            SelectedCartItem.Product.QuantityInStock += 1;

            if (SelectedCartItem.QuantityInCart > 1)
            {
                SelectedCartItem.QuantityInCart -= 1;
            }
            else
            {
                Cart.Remove(SelectedCartItem);
            }

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
            NotifyOfPropertyChange(() => CanAddToCart);
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProductsAsync();
        }

        private decimal CalculateSubTotal() => Cart.Sum(i => i.Product.RetailPrice * i.QuantityInCart);

        private decimal CalculateTax() => Cart.Sum(i => i.Product.RetailPrice * i.QuantityInCart * (i.Product.IsTaxable ? _configHelper.GetTaxRate() : 0));

        private async Task LoadProductsAsync() => Products = new BindingList<ProductDisplayModel>(_mapper.Map<List<ProductDisplayModel>>(await _productEndpoint.GetAllAsync()));

        private async Task ResetSalesViewModelASync()
        {
            Cart.Clear();
            await LoadProductsAsync();

            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }
    }
}