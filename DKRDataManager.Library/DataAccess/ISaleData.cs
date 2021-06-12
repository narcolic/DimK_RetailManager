using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public interface ISaleData
    {
        List<SaleReportModel> GetSalesReport();
        void SaveSale(SaleModel saleInfo, string cashierId);
    }
}