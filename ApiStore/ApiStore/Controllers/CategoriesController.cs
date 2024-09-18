using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Models.Category;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ApiStoreDbContext context, IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetList()
        {
            var list = context.Categories.ToList();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CategoryCreateViewModel model)
        {
            if (model == null) return BadRequest();

            string fname = Guid.NewGuid().ToString() + ".webp";
            var dir = configuration["ImagesDir"];

            using (MemoryStream ms = new())
            {
                await model.Image.CopyToAsync(ms);
                var bytes = ms.ToArray();
                int[] sizes = [50, 150, 300, 600, 1200];
                foreach (var size in sizes)
                {
                    string dirSave = Path.Combine(Directory.GetCurrentDirectory(),
                        dir, $"{size}_{fname}");
                    using (var image = Image.Load(bytes))
                    {
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(size, size),
                            Mode = ResizeMode.Max
                        }));
                        
                        image.Save(dirSave, new WebpEncoder());
                    }
                }
            }

            //var item = new CategoryEntity
            //{
            //    Name = model.Name,
            //    Description = model.Description,
            //    Image = fname
            //};

            //context.Categories.Add(item);
            //context.SaveChanges();

            return Ok();
        }


    }
}