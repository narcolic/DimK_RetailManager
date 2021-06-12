using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DKRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryData _inventoryData;

        public InventoryController(IInventoryData inventoryData)
        {
            _inventoryData = inventoryData;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public List<InventoryModel> Get() => _inventoryData.GetInventory();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post(InventoryModel item) => _inventoryData.SaveInventoryRecord(item);
    }
}