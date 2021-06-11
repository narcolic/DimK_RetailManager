using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class SaleData
    {
        private readonly IConfiguration _config;

        public SaleData(IConfiguration config)
        {
            _config = config;
        }

        public List<SaleReportModel> GetSalesReport() => new SqlDataAccess(_config).LoadData<SaleReportModel, dynamic>("dbo.spSale_SaleReport", new { }, "DKRData");

        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            var saleDetails = new List<SaleDetailDbModel>();
            var products = new ProductData(_config);

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDbModel()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var productInfo = products.GetProductById(detail.ProductId);
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

            using (SqlDataAccess sql = new SqlDataAccess(_config))
            {
                try
                {
                    sql.StartTransaction("DKRData");
                    int saleId = sql.SaveDataScalarInTransaction("dbo.spSale_Insert", sale);

                    saleDetails.ForEach(item =>
                    {
                        item.SaleId = saleId;
                        sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
                    });
                }
                catch
                {
                    sql.RollBackTransaction();
                    throw;
                }
            }
        }
    }
}