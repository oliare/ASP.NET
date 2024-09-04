using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebHulk.Data;
using WebHulk.Models.Categories;

namespace WebHulk.Controllers
{
    public class MainController : Controller
    {
        private readonly HulkDbContext context;
        private readonly IMapper _mapper;

        public MainController(HulkDbContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = context.Categories
                .ProjectTo<CategoryItemViewModel>(_mapper.ConfigurationProvider)
                //.Select(x => new CategoryItemViewModel
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    Image = x.Image
                //})
                .ToList();

            if (categories == null) Console.WriteLine("Categories is null..");
            return View(categories);

        }
    }
}
