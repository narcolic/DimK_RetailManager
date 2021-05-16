using DKRDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DKRDesktopUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task AddUserToRoleAsync(string userId, string role);

        Task<List<UserModel>> GetAllAsync();

        Task<Dictionary<string, string>> GetAllRolesAsync();

        Task RemoveUserFromRoleAsync(string userId, string role);
    }
}