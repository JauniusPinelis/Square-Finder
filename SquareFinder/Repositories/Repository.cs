using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SquareFinder.Models;
using SquareFinder.Db;
using System.Data.Entity;

namespace SquareFinder.Repositories
{
    public class Repository<T> where T : class
    {
        internal Context context;
        internal DbSet<T> table;
       
        public Repository(Context context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return table;
        }

        public virtual void Add(T entity)
        {
            table.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            table.Remove(entity);
        }

        public T Find(int id)
        {
            return table.Find(id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Context GetDb()
        {
            return context;
        }

    }
}