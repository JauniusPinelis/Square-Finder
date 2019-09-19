using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquareFinder.Models
{
    [Table("PointList")]
    public class PointList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<PointEntity> Points { get; set; }

        public PointList()
        {
            Points = new List<PointEntity>();
        }

        public PointList(string name)
        {
            Name = name;
            Points = new List<PointEntity>();
        }
    }
}