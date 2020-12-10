using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SODV3201_LibMgtSys.Models;

namespace SODV3201_LibMgtSys.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;

        public AccountController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public ViewResult Register()
        {
            return View();
        }

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
    }
}