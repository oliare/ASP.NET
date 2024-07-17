using Microsoft.EntityFrameworkCore;
using WebHulk.DATA.Entities;

namespace WebHulk.Data
{
    public class HulkDbContext : DbContext
    {
        public HulkDbContext(DbContextOptions<HulkDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
