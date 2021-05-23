using DKRApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DKRApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //string[] roles = { "Admin", "Manager", "Cashier" };
            //foreach (var role in roles)
            //{
            //    if (await _roleManager.RoleExistsAsync(role))
            //    {
            //        continue;
            //    }

            //    await _roleManager.CreateAsync(new IdentityRole(role));
            //}

            //var user = await _userManager.FindByEmailAsync("ddd@gmail.com");

            //if (user != null)
            //{
            //    await _userManager.AddToRoleAsync(user, "Admin");
            //    await _userManager.AddToRoleAsync(user, "Cashier");
            //}

            return View();
        }
    }
}