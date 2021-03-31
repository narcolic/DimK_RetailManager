using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace DKRDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public UserModel GetById()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userID).First();
        }
    }
}