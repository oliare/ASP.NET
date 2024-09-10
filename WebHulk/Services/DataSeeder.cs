﻿using Bogus;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Runtime.Intrinsics.X86;
using WebHulk.Constants;
using WebHulk.Data;
using WebHulk.Data.Entities;
using WebHulk.Data.Entities.Identity;
using WebHulk.DATA.Entities;
using WebHulk.Interfaces;

namespace WebHulk.Services
{
    public class DataSeeder
    {
        private readonly HulkDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IImageWorker _imageWorker;

        public DataSeeder(HulkDbContext context, UserManager<UserEntity> userManager,
            RoleManager<RoleEntity> roleManager, IImageWorker imageWorker)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _imageWorker = imageWorker;

        }

        public void SeedProducts()
        {
            if (_context.Products.Count() == 0)
            {
                int number = 10;
                var listCategories = new Faker().Commerce.Categories(number);

                string url = "https://picsum.photos/1200/800?category";
                foreach (var categoryName in listCategories)
                {
                    var imgName = _imageWorker.ImageSave(url);
                    if (!String.IsNullOrEmpty(imgName))
                    {
                        var catEntity = new CategoryEntity
                        {
                            Name = categoryName,
                            Image = imgName,
                        };
                        _context.Add(catEntity);
                        _context.SaveChanges();
                    }

                }

                var categories = _context.Categories.ToList();

                var fakerProduct = new Faker<Product>()
                    .RuleFor(u => u.Name, (f, u) => f.Commerce.Product())
                    .RuleFor(u => u.Price, (f, u) => decimal.Parse(f.Commerce.Price()))
                    .RuleFor(u => u.Category, (f, u) => f.PickRandom(categories));

                url = "https://picsum.photos/1200/800?product";

                var products = fakerProduct.GenerateLazy(105);
                Random r = new Random();

                foreach (var product in products)
                {
                    _context.Add(product);
                    _context.SaveChanges();
                    int imageCount = r.Next(3, 5);
                    for (int i = 0; i < imageCount; i++)
                    {
                        var imageName = _imageWorker.ImageSave(url);
                        var imageProduct = new ProductImage
                        {
                            Product = product,
                            Image = imageName,
                            Priotity = i
                        };
                        _context.Add(imageProduct);
                        _context.SaveChanges();
                    }

                }
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
