using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class InventoryData : IInventoryData
    {
        private readonly ISqlDataAccess _sql;

        public InventoryData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<InventoryModel> GetInventory() => _sql.LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "DKRData");

        public void SaveInventoryRecord(InventoryModel item) => _sql.SaveData("dbo.spInventory_Insert", item, "DKRData");
    }
}