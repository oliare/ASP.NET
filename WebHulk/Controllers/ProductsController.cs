using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;
using WebHulk.Models.Products;
using AutoMapper.QueryableExtensions;
using WebHulk.Data.Entities;

namespace WebHulk.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HulkDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(HulkDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var list = _context.Products
                  .ProjectTo<ProductItemViewModel>(_mapper.ConfigurationProvider)
                  .ToList() ?? throw new Exception("Failed to get products");

            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories
                .Select(x => new { Value = x.Id, Text = x.Name })
                .ToList();

            ProductCreateViewModel viewModel = new()
            {
                CategoryList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(categories, "Value", "Text")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var prod = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            await _context.Products.AddAsync(prod);
            await _context.SaveChangesAsync();

            if (model.Photos != null)
            {
                foreach (var img in model.Photos)
                {
                    string ext = Path.GetExtension(img.FileName);

                    string fName = Guid.NewGuid().ToString() + ext;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "images", fName);

                    using (var fs = new FileStream(path, FileMode.Create))
                        await img.CopyToAsync(fs);

                    var imgEntity = new ProductImage
                    {
                        Image = fName,
                        Product = prod,
                    };
                    _context.ProductImages.Add(imgEntity);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }

}

