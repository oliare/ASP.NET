using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;
using WebHulk.DATA.Entities;
using WebHulk.Models.Categories;

namespace WebHulk.Controllers
{
    public class MainController : Controller
    {
        private readonly HulkDbContext context;

        public MainController(HulkDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel createModel)
        {
            if (!ModelState.IsValid)
                return View(createModel);

            context.Categories.Add(new CategoryEntity
            {
                Name = createModel.Name,
                Image = createModel.Image
            });
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
            
        public IActionResult Index()
        {
            var categories = context.Categories
                .Select(x => new CategoryItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image
                })
                .ToList();

            if (categories == null) Console.WriteLine("Categories is null..");
            return View(categories);
        }
    }
}
