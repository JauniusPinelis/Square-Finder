using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquareFinder.Models
{
    [Table("PointLists")]
    public class PointList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Point> Points { get; set; }

        public PointList()
        {
            Points = new List<Point>();
        }

        public PointList(string name)
        {
            Name = name;
            Points = new List<Point>();
        }
    }
}