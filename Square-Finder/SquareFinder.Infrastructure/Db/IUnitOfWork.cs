using SquareFinder.Infrastructure.Entities;
using SquareFinder.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquareFinder.Infrastructure.Db
{
    public interface IUnitOfWork
    {
        IGenericRepository<PointEntity> PointsRepository { get; }

        IGenericRepository<PointListEntity> PointListsRepository { get; }

        void Save();
    }

}
