using System.Collections.Generic;
using System.Linq;

namespace SquareFinder.Models
{
    public class StateInformation
    {
        public IEnumerable<Point> Points { get; set; }
        public IEnumerable<Square> Squares { get; set; }
        public IEnumerable<PointList> PointLists { get; set; }
        
        public StateInformation()
        {

        }
        
        public StateInformation(int pointsListId, IQueryable<PointList> pointLists)
        {
            var pointList = pointLists.SingleOrDefault(x => x.Id == pointsListId);
            if (pointList != null)
            {
                Points = pointList.Points;
                Squares = Square.GetSquares(Points.ToList());
            }
            else
            {
                Points = new List<Point>();
                Squares = new List<Square>();
            }
                
            PointLists = pointLists.ToList();

        } 
    }
}