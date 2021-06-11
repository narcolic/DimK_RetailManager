using DKRApi.Data;
using DKRApi.Models;
using DKRDataManager.Library.DataAccess;
using DKRDataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/AddRole")]
        public async Task AddRoleAsync(UserRolePairModel pairModel)
        {
            var user = await _userManager.FindByIdAsync(pairModel.UserId);
            await _userManager.AddToRoleAsync(user, pairModel.Role);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
            return _context.Roles.ToDictionary(role => role.Id, role => role.Name);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/User/Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            var userRoles = _context.UserRoles.Join(_context.Roles.ToList(),
                    userRole => userRole.RoleId,
                    role => role.Id,
                    (userRole, role) => new { userRole.UserId, userRole.RoleId, role.Name });

            return _context.Users.Select(u => new ApplicationUserModel()
            {
                Id = u.Id,
                Email = u.Email,
                Roles = userRoles.Where(ur => ur.UserId == u.Id).ToDictionary(key => key.RoleId, val => val.Name)
            }).ToList();
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData(_config);

            return data.GetUserById(userID).First();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/User/Admin/RemoveRole")]
        public async Task RemoveRoleAsync(UserRolePairModel pairModel)
        {
            var user = await _userManager.FindByIdAsync(pairModel.UserId);
            await _userManager.RemoveFromRoleAsync(user, pairModel.Role);
        }
    }
}