using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServerMVC.Models;
using IdentityServerMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerMVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager
        )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["title"] = "Register";
            
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = new AppUser
            {
                Name = vm.Name,
                Email = vm.Email,
                UserName = vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (! result.Succeeded)
            {
                AddErrorsFromIdentityResult(result);
                return View(vm);
            }

            await _userManager.AddClaimAsync(user, new Claim("username", user.UserName));
            await _userManager.AddClaimAsync(user, new Claim("name", user.Name));
            await _userManager.AddClaimAsync(user, new Claim("email", user.Email));

            return RedirectToAction(nameof(Login));
        }

        private void AddErrorsFromIdentityResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("IdentityResult", error.Description);
        }
    }
}