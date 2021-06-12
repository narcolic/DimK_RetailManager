using DKRApi.Data;
using DKRApi.Models;
using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DKRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IUserData _userData;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IUserData userData, ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRole")]
        public async Task AddRole(UserRolePairModel pairModel)
        {
            string loggedInUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(pairModel.UserId);

            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}", loggedInUserID, pairModel.UserId, pairModel.Role);

            await _userManager.AddToRoleAsync(user, pairModel.Role);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles() => _context.Roles.ToDictionary(role => role.Id, role => role.Name);

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            var userRoles = from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.Id
                            select new { ur.UserId, ur.RoleId, r.Name };

            return _context.Users.ToList().Aggregate(new List<ApplicationUserModel>(), (output, user) =>
            {
                ApplicationUserModel u = new ApplicationUserModel
                {
                    Id = user.Id,
                    Email = user.Email
                };

                u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(key => key.RoleId, val => val.Name);
                output.Add(u);
                return output;
            });
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return _userData.GetUserById(userID).First();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRole")]
        public async Task RemoveRole(UserRolePairModel pairModel)
        {
            string loggedInUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(pairModel.UserId);

            _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}", loggedInUserID, pairModel.UserId, pairModel.Role);

            await _userManager.RemoveFromRoleAsync(user, pairModel.Role);
        }
    }
}