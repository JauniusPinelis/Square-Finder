using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SquareFinder.Models;

namespace SquareFinder.Db
{
    public interface IContext
    {
        IDbSet<Point> Points { get; }
        IDbSet<PointList> PointLists { get; }
        int SaveChanges();
    }
}