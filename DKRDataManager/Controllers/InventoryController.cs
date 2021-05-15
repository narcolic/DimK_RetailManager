using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace DKRDataManager.Controllers
{
    [Authorize]
    public class InventoryController : ApiController
    {
        public List<InventoryModel> Get() => new InventoryData().GetInventory();

        public void Post(InventoryModel item) => new InventoryData().SaveInventoryRecord(item);
    }
}