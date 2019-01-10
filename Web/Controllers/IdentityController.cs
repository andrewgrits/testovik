using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Identity;

namespace Web.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<UserEntity> userManager;
        private readonly SignInManager<UserEntity> signInManager;

        public IdentityController(UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new UserEntity
            {
                UserName = userModel.Login,
                Email = userModel.Email,
                Name = userModel.Name
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            if (!result.Succeeded)
            {
                TempData["error"] = "Ошибка при создании аккаунта, попробуйте ещё раз";
                return View();
            }

            await signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var hasLoggedIn = false;

            if (model.Login.Contains('@'))
            {
                hasLoggedIn = await LoginByEmail(model);
            }
            else
            {
                hasLoggedIn = await LoginByUserName(model);
            }

            if (!hasLoggedIn)
            {
                ModelState.AddModelError(nameof(model.Login), "Неверный логин или пароль");
                return View();
            }

            TempData["success"] = "Добро пожаловать";
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task<bool> LoginByUserName(LoginViewModel model)
        {
            var user = await userManager.FindByNameAsync(model.Login);

            return await TryLogin(user, model.Password);
        }

        private async Task<bool> LoginByEmail(LoginViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Login);

            return await TryLogin(user, model.Password);
        }

        private async Task<bool> TryLogin(UserEntity user, string password)
        {
            if (user == null)
            {
                return false;
            }

            var result = await signInManager.PasswordSignInAsync(user, password, true, false);

            return result.Succeeded;
        }
    }
}