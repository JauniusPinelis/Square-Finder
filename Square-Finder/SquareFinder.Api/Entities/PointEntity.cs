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

        public PointListEntity PointList { get; set; }
        public int PointListId { get; set; }

        public PointEntity()
        {

        }

        public PointEntity(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsValid(PointListEntity pointList)
        {
            if (pointList == null)
            {
                return false;
            }
            if (pointList.Points.Count() >= 10000)
            {
                return false;
            }
            if (X < -5000 || X > 5000 || Y < -5000 || Y > 5000)
            {
                return false;
            }
            if (pointList.Points.Contains(this))
            {
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