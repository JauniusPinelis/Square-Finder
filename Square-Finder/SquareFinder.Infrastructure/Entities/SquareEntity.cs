﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SquareFinder.Infrastructure.Entities
{
    public class SquareEntity : IEquatable<SquareEntity>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<PointEntity> Points { get; set; }

        public SquareEntity()
        {

        }

        public SquareEntity(PointEntity firstPoint, PointEntity secondPoint, PointEntity thirdPoint, PointEntity fourthPoint)
        {
            
            Points = new List<PointEntity> {firstPoint, secondPoint, thirdPoint, fourthPoint};

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
        public static List<SquareEntity> GetSquares(List<PointEntity> points)
        {
            var squares = new List<SquareEntity>();

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

                    var pointNeeded1 = new PointEntity(x3, y3);
                    var pointNeeded2 = new PointEntity(x4, y4);

                        
                    if (points.Contains(pointNeeded1) && points.Contains(pointNeeded2))
                    {
                        var newSquare = new SquareEntity(point1, point2, pointNeeded1, pointNeeded2);
                        if (!squares.Contains(newSquare)){
                            squares.Add(newSquare);
                        }
                    }

                        x3 = point2.X + dy;
                        y3 = point2.Y - dx;

                        x4 = x3 - dx;
                        y4 = y3 - dy;

                    pointNeeded1 = new PointEntity(x3, y3);
                    pointNeeded2 = new PointEntity(x4, y4);

                    if (points.Contains(pointNeeded1) && points.Contains(pointNeeded2))
                    {
                        var newSquare = new SquareEntity(point1, point2, pointNeeded1, pointNeeded2);
                        if (!squares.Contains(newSquare))
                        {
                            squares.Add(newSquare);
                        }
                    }                            
                }
            }
            return squares.ToList();
        }

        public bool Equals(SquareEntity square)
        {
            if (square == null)
            {
                return false;
            }

            return Points.SequenceEqual(square.Points);
        }
    }
}
 