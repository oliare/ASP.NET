using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.X86;
using WebHulk.Constants;
using WebHulk.Data;
using WebHulk.Data.Entities;
using WebHulk.Data.Entities.Identity;
using WebHulk.DATA.Entities;

namespace WebHulk.Services
{
    public class DataSeeder
    {
        private readonly HulkDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        public DataSeeder(HulkDbContext context, UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedProducts()
        {
            if (_context.Products.Count() == 0)
            {
                var c1 = new CategoryEntity { Name = "Laptops", Image = "eb47fa37-007c-4d3e-be5e-a5ccb7600320.jpg" };

                _context.Categories.Add(c1);
                _context.SaveChanges();

                var p1 = new Product { Name = "Ноутбук HP EliteBook 840 G10", CategoryId = c1.Id, Price = 2350.00m };
                var p2 = new Product { Name = "Ноутбук Dell Latitude 7640", CategoryId = c1.Id, Price = 2020.00m };

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

        public async Task SeedRolesAndUsers()
        {
            // seed roles
            if (_context.Roles.Count() == 0)
            {
                var roles = new[]
                {
                    new RoleEntity { Name = Roles.Admin },
                    new RoleEntity { Name = Roles.User }
                };

                foreach (var role in roles)
                {
                    var outcome = await _roleManager.CreateAsync(role);
                    if (!outcome.Succeeded) Console.WriteLine($"Failed to create role: {role.Name}");
                }
            }

            // seed users
            if (_context.Users.Count() == 0)
            {
                var users = new[]
               {
                    new { User = new UserEntity { FirstName = "Tony", LastName = "Stark", UserName = "admin1", Email = "admin@gmail.com" }, Password = "admin1", Role = Roles.Admin },
                    new { User = new UserEntity { FirstName = "Boba", LastName = "Gray", UserName = "user1", Email = "user@gmail.com" }, Password = "bobapass1", Role = Roles.User },
                    new { User = new UserEntity { FirstName = "Biba", LastName = "Undefined", UserName = "user2", Email = "biba@gmail.com" }, Password = "bibapass3", Role = Roles.User }
                };

                foreach (var i in users)
                {
                    var outcome = await _userManager.CreateAsync(i.User, i.Password);

                    if (!outcome.Succeeded) Console.WriteLine($"Failed to create user: {i.User.UserName}");
                    else await _userManager.AddToRoleAsync(i.User, i.Role);
                }
            }
        }

    }
}
