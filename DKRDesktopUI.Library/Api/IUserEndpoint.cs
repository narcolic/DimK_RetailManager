using DKRDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DKRDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task<List<UserModel>> GetAllAsync();
    }
}