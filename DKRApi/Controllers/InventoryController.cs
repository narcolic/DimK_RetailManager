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
        [Authorize(Roles = "Admin,Manager")]
        public List<InventoryModel> Get() => new InventoryData().GetInventory();

        [Authorize(Roles = "Admin")]
        public void Post(InventoryModel item) => new InventoryData().SaveInventoryRecord(item);
    }
}