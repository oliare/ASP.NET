using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Constants;

namespace WebHulk.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Admin)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}