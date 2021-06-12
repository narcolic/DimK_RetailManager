using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sql;

        public ProductData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<ProductModel> Products => _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DKRData");

        public ProductModel GetProductById(int productId) => _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "DKRData").FirstOrDefault();
    }
}