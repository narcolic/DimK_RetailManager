using DKRDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace DKRDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> AuthenticateAsync(string username, string password);

        Task GetLoggedInUserInfo(string token);
    }
}