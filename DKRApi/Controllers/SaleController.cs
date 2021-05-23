using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DKRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport() => new SaleData().GetSalesReport();

        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            if (sale is null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            new SaleData().SaveSale(sale, userId);
        }
    }
}