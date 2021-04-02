using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            return new SqlDataAccess().LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DKRData");
        }
    }
}