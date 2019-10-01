using SquareFinder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareFinder.Infrastructure.Repositories
{
    public interface IDbRepository
    {
        IEnumerable<PointListEntity> GetPointLists();
        PointListEntity GetPointListById(int pointListId);
        void RemovePointList(int pointListId);
        void AddPointList(PointListEntity pointList);
        void OverwritePointList(int pointListId, PointListEntity pointList);

        PointEntity GetPointById(int pointId);
        void DeletePoint(PointEntity point);
        void AddPoint(PointEntity point);

        void ImportPoints(string data, int listId);
        void DeletePoints(string data, int listId);
        IEnumerable<PointEntity> ConvertDataToPoints(string data);

        StateInfo GetStateInfo();
        void SaveChanges();
    }
}
