using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<UserModel> GetUserById(string Id) => _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id }, "DKRData");
    }
}