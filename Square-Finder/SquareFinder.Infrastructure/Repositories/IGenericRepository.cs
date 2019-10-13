using System;
using System.Collections.Generic;
using System.Text;

namespace SquareFinder.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Delete(int id);
        void Save();

    }
}
