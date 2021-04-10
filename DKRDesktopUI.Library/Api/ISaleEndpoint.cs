using DKRDesktopUI.Library.Models;
using System.Threading.Tasks;

namespace DKRDesktopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSaleAsync(SaleModel sale);
    }
}