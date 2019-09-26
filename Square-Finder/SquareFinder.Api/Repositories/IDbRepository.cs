using Microsoft.EntityFrameworkCore;
using SquareFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareFinder.Api.Repositories
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
        void AddPoint(int pointListId, PointEntity point);

        string ImportPoints(string data, string listId);
        string DeletePoints(string data, string listId, DbContext db);
        IEnumerable<PointEntity> ConvertDataToPoints(string data);

        StateInfo GetStateInfo();
        void SaveChanges();
    }
}
