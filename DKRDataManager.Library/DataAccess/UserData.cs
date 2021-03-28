using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string id)
        {
            var parameters = new { Id = id };

            return new SqlDataAccess().LoadData<UserModel, dynamic>("dbo.spUserLookup", parameters, "DKRData");
        }
    }
}