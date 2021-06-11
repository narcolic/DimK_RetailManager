using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class InventoryData
    {
        private readonly IConfiguration _config;

        public InventoryData(IConfiguration config)
        {
            _config = config;
        }

        public List<InventoryModel> GetInventory() => new SqlDataAccess(_config).LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "DKRData");

        public void SaveInventoryRecord(InventoryModel item) => new SqlDataAccess(_config).SaveData("dbo.spInventory_Insert", item, "DKRData");
    }
}