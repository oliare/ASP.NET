using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;

namespace WebHulk.Controllers
{
    public class MainController : Controller
    {
        private readonly HulkDbContext context;

        public MainController(HulkDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            if (categories == null) Console.WriteLine("Categories is null..");
            return View(categories);
        }
    }
}
