
using AutoMapper;
using SquareFinder.Core.Models;
using SquareFinder.Infrastructure.Db;
using SquareFinder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SquareFinder.Infrastructure.Repositories
{
    public class DbRepository : IDbRepository
    {
        private PointContext _dbContext;
        private IMapper _mapper;

        public DbRepository(PointContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public void ImportPoints(string data, int listId)
        {
            var pointList = GetPointListById(listId);

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
        }

        public void DeletePoints(string data, int listId)
        {
            var pointList = GetPointListById(listId);
            if (pointList == null) return;
            var pointsToDelete = ConvertDataToPoints(data);

            foreach (var point in pointsToDelete)
            {
                var pointToDelete = pointList.Points.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
                if (pointToDelete != null)
                    DeletePoint(pointToDelete);
                
                  
            }        
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
            _dbContext.PointLists.Add(pointList);
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
            if (pointList != null)
            {
                _dbContext.Points.Add(point);
            }

        }

        public IEnumerable<PointEntity> GetPoints()
        {
            return _dbContext.Points;
        } 

        public StateInfo GetStateInfo()
        {

            var state = new StateInfo()
            {
                Points = _mapper.Map<List<PointDto>>(GetPoints()),
                PointLists = _mapper.Map<List<PointListDto>>(GetPointLists()),
                Squares = new List<SquareDto>()
            };

            return state;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
