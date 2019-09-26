using Microsoft.EntityFrameworkCore;
using SquareFinder.Api.Db;
using SquareFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SquareFinder.Api.Repositories
{
    public class DbRepository : IDbRepository
    {
        private PointContext _dbContext;

        public DbRepository(PointContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ImportPoints(string data, string listId)
        {
            var currentListId = int.Parse(listId);

            var pointList = GetPointListById(currentListId);
            if (pointList != null)
            {
                var newPointsList = pointList.Points;

                var pointsToAdd = ConvertDataToPoints(data);

                foreach (var point in pointsToAdd)
                {
                    if (point.IsValid(pointList))
                    {
                        newPointsList.Add(point);
                    }
                }
            }

            _dbContext.SaveChanges();
            return errorBuilder.ToString();
        }

        public void DeletePoints(string data, string listId, DbContext db)
        {
            int currentListId = int.Parse(listId);
            var pointList = GetPointListById(currentListId);
            if (pointList == null) return;
            var pointsToDelete = ConvertDataToPoints(data);

            foreach (var point in pointsToDelete)
            {
                var pointToDelete = pointList.Points.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
                if (pointToDelete != null)
                    DeletePoint(pointToDelete);
                
                  
            }
            db.SaveChanges();

            
        }

        public IEnumerable<PointEntity> ConvertDataToPoints(string data)
        {
            var points = new List<PointEntity>();

            string[] pointsData = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var point in pointsData)
            {
                if (point != String.Empty)
                {
                    int x;
                    int y;
                    string[] pointData = point.Split(null);
                    bool isXConverted = int.TryParse(pointData[0], out x);
                    bool isYConverted = int.TryParse(pointData[1], out y);
                    if (isXConverted && isYConverted)
                        points.Add(new PointEntity(x, y));
                    else
                    {
                       // error
                    }
                }
            }
            return points;

        }

        public IEnumerable<PointListEntity> GetPointLists()
        {
            return _dbContext.PointLists;
        }

        public PointListEntity GetPointListById(int pointListId)
        {
            return _dbContext.PointLists.SingleOrDefault(p => p.Id == pointListId);
        }

        public void RemovePointList(int pointListId)
        {
            var pointList = GetPointListById(pointListId);

            if (pointList != null)
            {
                _dbContext.PointLists.Remove(pointList);
            }
        }

        public void AddPointList(PointListEntity pointList)
        {
            _dbContext.Add(pointList);
        }

        public void OverwritePointList(int pointListId, PointListEntity pointList)
        {
            var existingPointList = GetPointListById(pointListId);
            existingPointList = pointList;
        }

        public PointEntity GetPointById(int pointId)
        {
            return _dbContext.Points.SingleOrDefault(p => p.Id == pointId);
        }

        public void DeletePoint(PointEntity point)
        {
            _dbContext.Points.Remove(point);
        }

        public void AddPoint(int pointListId, PointEntity point)
        {
            var pointList = GetPointListById(pointListId);
            pointList.Points.Add(point);

        }

        public IEnumerable<PointEntity> GetPoints()
        {
            return _dbContext.Points;
        } 

        public StateInfo GetStateInfo()
        {

            return new StateInfo()
            {
                Points = GetPoints(),
                PointLists = GetPointLists()
            };
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
