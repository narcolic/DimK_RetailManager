using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class SaleData : ISaleData
    {
        private readonly ISqlDataAccess _sql;
        private readonly IProductData _productData;

        public SaleData(ISqlDataAccess sql, IProductData productData)
        {
            _sql = sql;
            _productData = productData;
        }

        public List<SaleReportModel> GetSalesReport() => _sql.LoadData<SaleReportModel, dynamic>("dbo.spSale_SaleReport", new { }, "DKRData");

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            var saleDetails = new List<SaleDetailDbModel>();

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDbModel()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var productInfo = _productData.GetProductById(detail.ProductId);
                detail.PurchasePrice = productInfo.RetailPrice * detail.Quantity;
                detail.Tax = productInfo.IsTaxable ? detail.PurchasePrice * ConfigHelper.GetTaxRate() : detail.Tax;

                saleDetails.Add(detail);
            }

            var sale = new SaleDbModel()
            {
                SubTotal = saleDetails.Sum(d => d.PurchasePrice),
                Tax = saleDetails.Sum(d => d.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;


            try
            {
                _sql.StartTransaction("DKRData");
                int saleId = _sql.SaveDataScalarInTransaction("dbo.spSale_Insert", sale);

                saleDetails.ForEach(item =>
                {
                    item.SaleId = saleId;
                    _sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                });
            }
            catch
            {
                _sql.RollBackTransaction();
                throw;
            }

        }
    }
}