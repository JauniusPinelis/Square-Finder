using SquareFinder.Api.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SquareFinder.Models
{
    [Table("Point")]
    public class PointEntity : IComparable<PointEntity>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int X { get; set;}
        public int Y { get; set;}

        public PointEntity()
        {

        }

        public PointEntity(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsValid(PointList pointList, ref StringBuilder errorBuilder)
        {
            string err = $"Point x:{X} y:{Y} has not been added: ";
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

        public int CompareTo(PointEntity point)
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
            var item = obj as PointEntity;

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
        
    }
}