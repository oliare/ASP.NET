﻿using Microsoft.EntityFrameworkCore;
using WebHulk.Data.Entities;
using WebHulk.DATA.Entities;

namespace WebHulk.Data
{
    public class HulkDbContext : DbContext
    {
        public HulkDbContext(DbContextOptions<HulkDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)    
                .WithOne(pi => pi.Product)        
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId);

        }
    }

}
