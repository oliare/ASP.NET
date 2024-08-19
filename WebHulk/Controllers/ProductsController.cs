using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;
using WebHulk.Models.Products;
using AutoMapper.QueryableExtensions;
using WebHulk.DATA.Entities;
using WebHulk.Models.Categories;
using WebHulk.Data.Entities;
using System.Text.Json;

namespace WebHulk.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HulkDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(HulkDbContext context, IMapper mapper, ILogger<ProductsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index([FromRoute] int id)
        {
            var list = _context.Products
                  .ProjectTo<ProductItemViewModel>(_mapper.ConfigurationProvider)
                  .ToList() ?? throw new Exception("Failed to get products");

            return View(new ProdViewModel { CategoryId = id, Products = list });
        }

        public class ProdViewModel
        {
            public int CategoryId { get; set; }
            public List<ProductItemViewModel> Products { get; set; }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var prod = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            await _context.Products.AddAsync(prod, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            if (!string.IsNullOrEmpty(model.Base64Images))
            {
                var base64Images = new List<string>();

                try
                {
                    base64Images = JsonSerializer.Deserialize<List<string>>(model.Base64Images) ?? [];
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize base64 images");
                }

                foreach (var base64Image in base64Images)
                {
                    if (!string.IsNullOrWhiteSpace(base64Image))
                    {
                        // excluding the data:image/jpeg;base64, prefix
                        var newImg = base64Image.Split(',')[1];

                        // Decode the Base64 string to get the image bytes
                        var imageBytes = Convert.FromBase64String(newImg);
                        var uniqueFileName = Guid.NewGuid().ToString() + ".jpg"; // Assuming JPG format, adjust as needed
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", uniqueFileName);

                        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes, cancellationToken);
                        var img = new ProductImage
                        {
                            Image = uniqueFileName,
                            Product = prod,
                        };
                        _context.ProductImages.Add(img);
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }





        //[HttpPost]
        //public async Task<IActionResult> Create(ProductCreateViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var prod = new Product
        //    {
        //        Name = model.Name,
        //        Price = model.Price,
        //        CategoryId = model.CategoryId
        //    };

        //    _context.Products.Add(prod);
        //    await _context.SaveChangesAsync();

        //    foreach (var i in model.Images)
        //    {
        //        if (i.Length > 0)
        //        {
        //            var fName = Path.GetFileName(i.FileName);
        //            var path = Path.Combine(Directory.GetCurrentDirectory(), "images", fName);

        //            using (var stream = new FileStream(path, FileMode.Create))
        //                await i.CopyToAsync(stream);

        //            var img = new ProductImage
        //            {
        //                Image = fName,
        //                Product = prod
        //            };
        //            _context.ProductImages.Add(img);
        //        }
        //    }

        //    _context.SaveChanges();

        //    return RedirectToAction(nameof(Index));
        //}



    }

}

