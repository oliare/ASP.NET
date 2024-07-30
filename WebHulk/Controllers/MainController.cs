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

        [HttpGet]   
        public IActionResult Edit(int id)
        {
            var item = context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryEditViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image
                })
                .FirstOrDefault();

            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(CategoryEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var item = context.Categories.Find(model.Id);

            item.Name = model.Name;
            item.Image = model.Image;

            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = context.Categories.Find(id);

            if (item == null) return NotFound();

            context.Categories.Remove(item);
            context.SaveChanges();
                
            return RedirectToAction("Index");
        }
    }

}
