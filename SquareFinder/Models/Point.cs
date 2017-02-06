using SquareFinder.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SquareFinder.Models
{
    [Table("Points")]
    public class Point : IComparable<Point>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int X { get; set;}
        public int Y { get; set;}

        public Point()
        {

        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsValid(IContext db, int pointListId, ref StringBuilder errorBuilder)
        {
            string err = $"Point x:{X} y:{Y} has not been added: ";
            var pointList = db.PointLists.Find(pointListId);
            if (pointList == null)
            {
                errorBuilder.AppendLine(err + "PointList not found");
                return false;
            }
            if (pointList.Points.Count() >= 10000)
            {
                errorBuilder.AppendLine(err + "10 000 points limit reached");
                return false;
            }
            if (X < -5000 || X > 5000 || Y < -5000 || Y > 5000)
            {
                errorBuilder.AppendLine(err + "Point coordinates are Incorrect");
                return false;
            }
            if (pointList.Points.Contains(this))
            {
                errorBuilder.AppendLine(err + "Point already exists");
                return false;
            }
            return true;
        }

        public int CompareTo(Point point)
        {
            if (point == null)
                return 0;
            if (X < point.X)
                return -1;
            if (X > point.X)
                return 1;
            if (Y < point.Y)
                return -1;
            if (Y > point.Y)
                return 1;
            return 0;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Point;

            if (item == null)
            {
                return false;
            }

            return (X == item.X && Y == item.Y);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static IEnumerable<Point> ConvertDataToPoints(string data, ref StringBuilder errorBuilder)
        {
            var points = new List<Point>();

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
                        points.Add(new Point(x, y));
                    else
                    {
                        errorBuilder.AppendLine($"Point x:{pointData[0]} y:{pointsData[1]}");
                    }
                }       
            }
            return points;

        }

        public static string ImportPoints(string data, string listId, IContext db)
        {
            var errorBuilder = new StringBuilder();
            var currentListId = int.Parse(listId);

            var pointList = db.PointLists.Find(currentListId);
            if (pointList != null)
            {
                var pointsList = pointList.Points;
                
                var pointsToAdd = ConvertDataToPoints(data, ref errorBuilder);

                foreach (var point in pointsToAdd)
                {
                    if (point.IsValid(db, currentListId, ref errorBuilder))
                    {
                        pointsList.Add(point);
                    }
                }
            }

            db.SaveChanges();
            return errorBuilder.ToString();
        }

        public static string DeletePoints(string data, string listId, IContext db)
        {
            var errorBuilder = new StringBuilder();
            int currentListId = int.Parse(listId);
            var pointList = db.PointLists.Find(currentListId);
            if (pointList == null) return "Pointlist has not been found";
            var pointsToDelete = ConvertDataToPoints(data, ref errorBuilder);

            foreach (var point in pointsToDelete)
            {
                var pointToDelete = pointList.Points.FirstOrDefault(x => x.X == point.X && x.Y == point.Y);
                if (pointToDelete != null)
                    db.Points.Remove(pointToDelete);
                else
                    errorBuilder.AppendLine($"Point x:{point.X}, y:{point.Y} has not been found");
            }
            db.SaveChanges();

            return errorBuilder.ToString();
        }
    }
}