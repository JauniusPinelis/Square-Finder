
using Microsoft.EntityFrameworkCore;
using SquareFinder.Infrastructure.Entities;

namespace SquareFinder.Infrastructure.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<DbContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        public DbSet<PointEntity> Points { get; set; }
        public DbSet<PointListEntity> PointLists { get; set; }
    }
}