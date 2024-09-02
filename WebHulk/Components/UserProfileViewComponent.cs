using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Areas.Admin.Models.Profile;
using WebHulk.Constants;
using WebHulk.Data.Entities.Identity;

namespace WebHulk.Areas.Admin.Components
{
    [Authorize]
    public class UserProfileViewComponent : ViewComponent
    {
        private UserManager<UserEntity> _userManager;
        public UserProfileViewComponent(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ProfileItemViewModel model = new ProfileItemViewModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            //model.Role = user.UserRoles;
            //model.Image = string.IsNullOrEmpty(user.Image) ? "select.png" : user.Image;
            return View("Index", model);
        }
    }
}
