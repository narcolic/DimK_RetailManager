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
        private readonly ISaleData _saleData;

        public SaleController(ISaleData saleData)
        {
            _saleData = saleData;
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        [HttpGet]
        public List<SaleReportModel> GetSalesReport() => _saleData.GetSalesReport();

        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public void Post(SaleModel sale)
        {
            if (sale is null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _saleData.SaveSale(sale, userId);
        }
    }
}