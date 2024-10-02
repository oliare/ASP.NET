using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Interfaces;
using ApiStore.Models.Category;
using ApiStore.Models.Product;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(ApiStoreDbContext context,
          IImageTool imageTool, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var list = context.Products.ProjectTo<ProductItemViewModel>(mapper.ConfigurationProvider).ToList();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateViewModel model)
        {
            var entity = mapper.Map<ProductEntity>(model);
            context.Products.Add(entity);
            context.SaveChanges();

            if (model.Images != null)
            {
                var p = 1;
                foreach (var image in model.Images)
                {
                    var pi = new ProductImageEntity
                    {
                        Image = await imageTool.Save(image),
                        Priority = p,
                        ProductId = entity.Id
                    };
                    p++;
                    context.ProductImages.Add(pi);
                    await context.SaveChangesAsync();
                }
            }

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] ProductEditViewModel model)
        {
            var product = mapper.Map<ProductEntity>(model);
            await context.SaveChangesAsync();

            var images = context.ProductImages.Where(x => x.ProductId == model.Id).ToList();
            if (images != null)
            {
                foreach (var img in images)
                {
                    if (!model.PreviousImages.Any(x => x.Image?.FileName == img.Image))
                    {
                        context.ProductImages.Remove(img);
                        imageTool.Delete(img.Image);
                    }
                    else
                        img.Priority = model.PreviousImages
                            .SingleOrDefault(x => x.Image?.FileName == img.Image)?.Priority ?? img.Priority;
                }

                if (model.NewImages != null)
                {
                    foreach (var img in model.NewImages)
                    {
                        var imagePath = await imageTool.Save(img.Image);
                        context.ProductImages.Add(new ProductImageEntity { Image = imagePath, ProductId = product.Id, Priority = img.Priority });
                    }
                }
            }
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = context.Products
                .Include(x => x.ProductImages)
                .SingleOrDefault(x => x.Id == id);
            
            if (product == null) return NotFound();

            if (product.ProductImages != null)
                foreach (var p in product.ProductImages)
                    imageTool.Delete(p.Image);

            context.Products.Remove(product);
            context.SaveChanges();
            return Ok();
        }

    }
}