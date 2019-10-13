using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SquareFinder.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquareFinder.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private DbSet<T> table = null;

        public GenericRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

            table = _context.Set<T>();
        }

        public void Delete(int id)
        {
            var point = GetById(id);
            table.Remove(point);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
