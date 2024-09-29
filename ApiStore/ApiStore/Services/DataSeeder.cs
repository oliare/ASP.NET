using ApiStore.Data;
using ApiStore.Data.Entities;
using ApiStore.Interfaces;
using Bogus;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiStore.Services
{
    public class DataSeeder
    {
        private readonly ApiStoreDbContext _context;
        private readonly IImageTool _imageTool;

        public DataSeeder(ApiStoreDbContext context, IImageTool imageTool)
        {
            _context = context;
            _imageTool = imageTool;
        }

        public async Task SeedCategories()
        {
            if (!_context.Categories.Any())
            {
                int number = 10;
                var list = new Faker("uk")
                    .Commerce.Categories(number);

                var categories = new List<CategoryEntity>();
                foreach (var name in list)
                {
                    string image = await _imageTool.SaveImageByUrl("https://picsum.photos/1200/800?category");
                    var cat = new CategoryEntity
                    {
                        Name = name,
                        Description = new Faker("uk").Commerce.ProductDescription(),
                        Image = image
                    };
                    categories.Add(cat);
                }

                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }
        }

        public async Task SeedProducts()
        {
            if (_context.Products.Any()) return;

            Faker faker = new Faker();
            var categories = _context.Categories.Select(c => c.Id).ToList();
            var url = "https://picsum.photos/1200/800?product";

            var fakeProduct = new Faker<ProductEntity>()
                .RuleFor(p => p.Name, f => f.Commerce.Product())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories));

            var products = fakeProduct.Generate(105);
            var r = new Random();

            _context.Products.AddRange(products);
            _context.SaveChanges();

            var productImages = new List<ProductImageEntity>();

            foreach (var product in products)
            {
                int count = r.Next(3, 5);
                for (int i = 0; i < count; i++)
                {
                    var fname = await _imageTool.SaveImageByUrl(url);
                    var imageProduct = new ProductImageEntity
                    {
                        ProductId = product.Id,
                        Image = fname,
                        Priority = i
                    };
                    _context.ProductImages.Add(imageProduct);
                }
            }
            _context.SaveChanges();
        }

        public async Task SeedData()
        {
            await SeedCategories();
            await SeedProducts();
        }

    }
}