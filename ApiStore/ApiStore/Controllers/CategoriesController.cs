using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ApiStoreDbContext context) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetList()
        {
            var list = context.Categories.ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create([FromForm] CategoryCreateViewModel model)
        {
            if (model == null) return BadRequest();

            string ext = Path.GetExtension(model.Image.FileName);
            string fname = Guid.NewGuid() + ext;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "images", fname);
            using (var stream = new FileStream(path, FileMode.Create))
                 model.Image.CopyTo(stream);

            var item = new CategoryEntity
            {
                Name = model.Name,
                Description = model.Description,
                Image = fname
            };

            context.Categories.Add(item);
            context.SaveChanges();

            return Ok(item);
        }
    }
}