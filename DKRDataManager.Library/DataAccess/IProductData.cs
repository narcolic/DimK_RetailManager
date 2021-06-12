using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public interface IProductData
    {
        List<ProductModel> Products { get; }

        ProductModel GetProductById(int productId);
    }
}