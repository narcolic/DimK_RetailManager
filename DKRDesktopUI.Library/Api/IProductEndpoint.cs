using DKRDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DKRDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAllAsync();
    }
}