using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public SaleController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSalesReport")]
        public List<SaleReportModel> GetSalesReport() => new SaleData(_config).GetSalesReport();

        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            if (sale is null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            new SaleData(_config).SaveSale(sale, userId);
        }
    }
}