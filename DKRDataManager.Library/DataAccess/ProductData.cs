using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DKRDataManager.Library.DataAccess
{
    public class ProductData
    {
        private readonly IConfiguration _config;

        public ProductData(IConfiguration config)
        {
            _config = config;
        }

        public List<ProductModel> Products => new SqlDataAccess(_config).LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "DKRData");

        public ProductModel GetProductById(int productId) => new SqlDataAccess(_config).LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "DKRData").FirstOrDefault();
    }
}