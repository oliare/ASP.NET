using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;
using WebHulk.Models.Products;
using AutoMapper.QueryableExtensions;

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

    }
}

