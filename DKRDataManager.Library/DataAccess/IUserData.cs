using DKRDataManager.Library.Models;
using System.Collections.Generic;

namespace DKRDataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}