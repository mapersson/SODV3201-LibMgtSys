using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SODV3201_LibMgtSys.Models;
using SODV3201_LibMgtSys.Models.ViewModels;

namespace SODV3201_LibMgtSys.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        public AccountController(UserManager<AppUser> usrMgr, SignInManager<AppUser> signInMgr)
        {

            userManager = usrMgr;
            signInManager = signInMgr;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            LoginData loginData = new LoginData();
            loginData.ReturnUrl = returnUrl;
            return View(loginData);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginData loginData)
        {

            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByEmailAsync(loginData.Email);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(appUser, loginData.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(loginData.ReturnUrl ?? "/");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(loginData.Email), "Login Failed, Invalid Email or Password");
                }

            }
            return View(loginData);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ViewResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser newUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                };

                IdentityResult result = await userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(user);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}