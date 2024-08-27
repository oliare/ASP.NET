using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebHalk.Models.Account;
using WebHulk.Constants;
using WebHulk.Data.Entities.Identity;

namespace WebHulk.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;

        public AccountController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };

                var outcome = await _userManager.CreateAsync(user, model.Password);
                if (outcome != null)
                {
                    if (!outcome.Succeeded)
                    {
                        outcome = await _userManager.AddToRoleAsync(user, Roles.User);
                    }
                }

                return RedirectToAction("Index", "Main");
            }

            return View(model);
        }
    }
}
