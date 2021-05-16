using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using DKRDataManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DKRDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/AddRole")]
        public void AddRole(UserRolePairModel pairModel)
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.AddToRole(pairModel.UserId, pairModel.Role);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Roles.ToDictionary(role => role.Id, role => role.Name);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var users = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)).Users.ToList();
                var roles = context.Roles.ToList();

                return users.Select(u => new ApplicationUserModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = u.Roles.Join(roles,
                        userRole => userRole.RoleId,
                        role => role.Id,
                        (userRole, role) => new { UserRole = userRole, Role = role }).ToDictionary(x => x.UserRole.RoleId, x => x.Role.Name)
                }).ToList();
            }
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userID = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userID).First();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/RemoveRole")]
        public void RemoveRole(UserRolePairModel pairModel)
        {
            using (var context = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                userManager.RemoveFromRole(pairModel.UserId, pairModel.Role);
            }
        }
    }
}