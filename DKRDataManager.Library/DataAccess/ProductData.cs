using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class ProductData
    {
        public ProductModel GetProductById(int productId)
        {
            return new SqlDataAccess().LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "DKRData").FirstOrDefault();
        }

        public List<ProductModel> GetProducts()
        {
            return new SqlDataAccess().LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DKRData");
        }
    }
}