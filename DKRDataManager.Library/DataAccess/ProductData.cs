using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> Products => new SqlDataAccess().LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DKRData");

        public ProductModel GetProductById(int productId) =>
                    new SqlDataAccess().LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "DKRData").FirstOrDefault();
    }
}