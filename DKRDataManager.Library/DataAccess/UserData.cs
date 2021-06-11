using DKRDataManager.Library.Internal.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public class UserData
    {
        private readonly IConfiguration _config;

        public UserData(IConfiguration config)
        {
            _config = config;
        }

        public List<UserModel> GetUserById(string id)
        {
            var parameters = new { Id = id };

            return new SqlDataAccess(_config).LoadData<UserModel, dynamic>("dbo.spUserLookup", parameters, "DKRData");
        }
    }
}