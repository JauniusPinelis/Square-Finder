
using Microsoft.EntityFrameworkCore;
using SquareFinder.Infrastructure.Entities;

namespace SquareFinder.Infrastructure.Db
{
    public class PointContext : DbContext
    {
        public PointContext(DbContextOptions<PointContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        public DbSet<PointEntity> Points { get; set; }
        public DbSet<PointListEntity> PointLists { get; set; }
    }
}