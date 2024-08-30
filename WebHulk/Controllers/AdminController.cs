using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHulk.Constants;
using WebHulk.Data.Entities.Identity;
using WebHulk.Models.Admin;

namespace WebHulk.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public AdminController(UserManager<UserEntity> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = _userManager.Users.ToList();
            var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin);

            var users = list
                .Where(user => !admins.Contains(user))
                .ToList();

            var model = _mapper.Map<List<UserItemViewModel>>(users);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser(string id)
        {
            await ToggleUserLockout(id, DateTimeOffset.MaxValue);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UnblockUser(string id)
        {
            await ToggleUserLockout(id, null);
            return RedirectToAction(nameof(Index));
        }

        private async Task ToggleUserLockout(string name, DateTimeOffset? end)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                user.LockoutEnd = end;
                await _userManager.UpdateAsync(user);
            }
        }

    }
}
