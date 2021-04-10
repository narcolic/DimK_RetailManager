using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Http;

namespace DKRDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            if (sale is null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            string userId = RequestContext.Principal.Identity.GetUserId();
            new SaleData().SaveSale(sale, userId);
        }
    }
}