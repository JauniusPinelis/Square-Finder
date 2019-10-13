using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SquareFinder.Infrastructure.Entities;
using SquareFinder.Infrastructure.Repositories;

namespace SquareFinder.Infrastructure.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public IGenericRepository<PointEntity> _pointsRepository;

        public IGenericRepository<PointListEntity> _pointListsRepository;

        public UnitOfWork(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

       public IGenericRepository<PointEntity> PointsRepository
        {
            get
            {
                return _pointsRepository ?? new GenericRepository<PointEntity>(_dbContext, _mapper);
            }
        }



             public IGenericRepository<PointListEntity> PointsListRepository
        {
            get
            {
                return _pointListsRepository ?? new GenericRepository<PointListEntity>(_dbContext, _mapper);
            }
        }
    }
}
