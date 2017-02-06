using System;
using System.Collections.Generic;
using System.Linq;

namespace SquareFinder.Models
{
    public class Square : IEquatable<Square>
    {
        public Guid Id { get; set; }
        public List<Point> Points { get; set; }

        public Square(Point firstPoint, Point secondPoint, Point thirdPoint, Point fourthPoint)
        {
            Id = Guid.NewGuid();
            Points = new List<Point> {firstPoint, secondPoint, thirdPoint, fourthPoint};

            //Sorting points to identify unique Squares later on
            Points.Sort();  
        }

        public override int GetHashCode()
        {
            return Points.GetHashCode();
        }
       
        /// <summary>
        /// The algorithm is simple: go every pair of points and find
        /// what points are needed to make a square. If those points are found
        /// in out pointList - mark them as a square.
        /// </summary>
        public static List<Square> GetSquares(List<Point> points)
        {
            var squares = new List<Square>();

            //Not enough points - there are no squares
            if (points.Count() < 4) 
            {
                return squares;
            }

            for (int i = 0; i < points.Count()-1; i++)
            {
                for (int j = i+1; j < points.Count(); j++)
                { 
                    var point1 = points.ElementAt(i);
                    var point2 = points.ElementAt(j);

                    int dx = point2.X - point1.X;
                    int dy = point2.Y - point1.Y;

                    var x3 = point2.X - dy;
                    var y3 = point2.Y + dx;

                    var x4 = x3 - dx;
                    var y4 = y3 - dy;

                    var pointNeeded1 = new Point(x3, y3);
                    var pointNeeded2 = new Point(x4, y4);

                        
                    if (points.Contains(pointNeeded1) && points.Contains(pointNeeded2))
                    {
                        var newSquare = new Square(point1, point2, pointNeeded1, pointNeeded2);
                        if (!squares.Contains(newSquare)){
                            squares.Add(newSquare);
                        }
                    }

                        x3 = point2.X + dy;
                        y3 = point2.Y - dx;

                        x4 = x3 - dx;
                        y4 = y3 - dy;

                    pointNeeded1 = new Point(x3, y3);
                    pointNeeded2 = new Point(x4, y4);

                    if (points.Contains(pointNeeded1) && points.Contains(pointNeeded2))
                    {
                        var newSquare = new Square(point1, point2, pointNeeded1, pointNeeded2);
                        if (!squares.Contains(newSquare))
                        {
                            squares.Add(newSquare);
                        }
                    }                            
                }
            }
            return squares.ToList();
        }

        public bool Equals(Square square)
        {
            if (square == null)
            {
                return false;
            }

            return Points.SequenceEqual(square.Points);
        }
    }
}
 