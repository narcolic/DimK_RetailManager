using DKRDesktopUI.Models;
using System.Threading.Tasks;

namespace DKRDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> AuthenticateAsync(string username, string password);
    }
}