using SquareFinder.Db;
using SquareFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SquareFinder.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private Context context = new Context();
        private Repository<Point> pointsRepository;
        private Repository<PointList> pointListsRepository;

        public Repository<Point> PointsRepository
        {
            get
            {
                if (pointsRepository == null)
                {
                    this.pointsRepository = new Repository<Point>(context);
                }
                return pointsRepository;
            }
        }

        public Repository<PointList> PointListsRepository
        {
            get
            {
                if (pointListsRepository == null)
                {
                    this.pointListsRepository = new Repository<PointList>(context);
                }
                return pointListsRepository;
            }
        }

        public UnitOfWork()
        {

        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Context GetContext()
        {
            return context;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}