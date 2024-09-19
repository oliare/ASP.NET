using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Models.Category;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ApiStoreDbContext context,
        IConfiguration configuration, IMapper mapper) : ControllerBase
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

            var item = mapper.Map<CategoryEntity>(model);

            string fname = await SaveImageAsync(model.Image);
            item.Image = fname;

            context.Categories.Add(item);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] CategoryEditViewModel model)
        {
            if (model == null) return NotFound();

            var ctgr = context.Categories.Find(model.Id);

            ctgr.Name = model.Name;
            ctgr.Description = model.Description;

            if (model.Image != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", ctgr.Image);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                string fname = await SaveImageAsync(model.Image);
                ctgr.Image = fname;
            }

            context.Categories.Update(ctgr);
            context.SaveChanges();

            return Ok(model);
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            string fname = Guid.NewGuid().ToString() + ".webp";
            var dir = configuration["ImagesDir"];

            using (MemoryStream ms = new())
            {
                await imageFile.CopyToAsync(ms);
                var bytes = ms.ToArray();
                int[] sizes = { 50, 150, 300, 600, 1200 };

                foreach (var size in sizes)
                {
                    string dirSave = Path.Combine(Directory.GetCurrentDirectory(), dir, $"{size}_{fname}");
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
            return fname;
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ctgr = context.Categories.Find(id);
            if (ctgr != null)
            {
                context.Categories.Remove(ctgr);
                context.SaveChanges();
            }
            return Ok();
        }

    }
}