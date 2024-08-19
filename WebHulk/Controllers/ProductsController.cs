using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;
using WebHulk.Models.Products;
using AutoMapper.QueryableExtensions;
using WebHulk.DATA.Entities;
using WebHulk.Models.Categories;
using WebHulk.Data.Entities;
using System.Text.Json;
using System;

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
            var list = _context.Products.Where(x => x.CategoryId == id)
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

            if (!string.IsNullOrEmpty(model.Images))
            {
                var arr = new List<string>();
                try
                {
                    arr = JsonSerializer.Deserialize<List<string>>(model.Images) ?? [];
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize base64 images");
                }

                foreach (var i in arr)
                {
                    if (!string.IsNullOrWhiteSpace(i))
                    {
                        // excluding the data --> image/jpeg;base64, prefix
                        var newImg = i.Split(',')[1];

                        var bytes = Convert.FromBase64String(newImg);
                        var fName = Guid.NewGuid().ToString() + ".jpg"; // !!!
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "images", fName);

                        await System.IO.File.WriteAllBytesAsync(filePath, bytes, cancellationToken);
                        var img = new ProductImage
                        {
                            Image = fName,
                            Product = prod,
                        };
                        _context.ProductImages.Add(img);
                    }
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Main");
        }

    }

}

