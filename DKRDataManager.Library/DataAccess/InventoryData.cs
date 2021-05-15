using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class InventoryData
    {
        public List<InventoryModel> GetInventory() => new SqlDataAccess().LoadData<InventoryModel, dynamic>("dbo.spInventory_GetAll", new { }, "DKRData");

        public void SaveInventoryRecord(InventoryModel item) => new SqlDataAccess().SaveData("dbo.spInventory_Insert", item, "DKRData");
    }
}