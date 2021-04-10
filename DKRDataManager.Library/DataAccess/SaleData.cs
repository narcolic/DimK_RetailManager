using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            var saleDetails = new List<SaleDetailDbModel>();
            var products = new ProductData();
            var sql = new SqlDataAccess();

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

            int saleId = sql.SaveDataScalar("dbo.spSale_Insert", sale, "DKRData");

            saleDetails.ForEach(item =>
            {
                item.SaleId = saleId;
                sql.SaveData("dbo.spSaleDetail_Insert", item, "DKRData");
            });
        }
    }
}