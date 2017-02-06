using SquareFinder.Models;
using System.Data.Entity;

namespace SquareFinder.Db
{
    public class Context : DbContext, IContext
    {
        public IDbSet<Point> Points { get; set; }
        public IDbSet<PointList> PointLists { get; set; }
    }
}