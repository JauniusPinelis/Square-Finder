using SquareFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SquareFinder.Repositories
{
    public interface IRepository
    {
        void Add(Point entity);
        void Remove(Point entity);

        List<Point> GetAll();
        Point Find(int id);

        void SaveChanges();
    }
}