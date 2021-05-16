using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DKRDataManager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get() => new ProductData().Products;
    }
}