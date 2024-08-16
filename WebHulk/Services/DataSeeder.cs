using WebHulk.Data;
using WebHulk.Data.Entities;

namespace WebHulk.Services
{
    public class DataSeeder
    {
        private readonly HulkDbContext _context;

        public DataSeeder(HulkDbContext context)
        {
            _context = context;
        }
        public void SeedProducts()
        {
            if (_context.Products.Count() == 0)
            {
                var p1 = new Product { Name = "Ноутбук HP EliteBook 840 G10", CategoryId = 28 };
                var p2 = new Product { Name = "Ноутбук Dell Latitude 7640", CategoryId = 28 };

                _context.Products.AddRange(p1, p2);

                _context.ProductImages.AddRange(
                    new ProductImage { Image = "p_1(1).webp", Product = p1 },
                    new ProductImage { Image = "p_1(2).webp", Product = p1 },
                    new ProductImage { Image = "p_1(3).webp", Product = p1 },

                    new ProductImage { Image = "p_2(1).webp", Product = p2 },
                    new ProductImage { Image = "p_2(2).webp", Product = p2 },
                    new ProductImage { Image = "p_2(3).webp", Product = p2 }
                );

                _context.SaveChanges();
            }
        }
    }
}
