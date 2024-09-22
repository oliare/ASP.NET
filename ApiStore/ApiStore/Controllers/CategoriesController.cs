using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Interfaces;
using ApiStore.Models.Category;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ApiStoreDbContext context,
       IImageTool imageTool, IMapper mapper) : ControllerBase
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

            string fname = await imageTool.Save(model.Image);
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

                string fname = await imageTool.Save(model.Image);
                ctgr.Image = fname;
            }

            context.Categories.Update(ctgr);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ctgr = context.Categories.SingleOrDefault(x => x.Id == id);
            if (ctgr == null) return NotFound();

            if (!string.IsNullOrEmpty(ctgr.Image))
                imageTool.Delete(ctgr.Image);

            context.Categories.Remove(ctgr);
            context.SaveChanges();
            return Ok();
        }

    }
}