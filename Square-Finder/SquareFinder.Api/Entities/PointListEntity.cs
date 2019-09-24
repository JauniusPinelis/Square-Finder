using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SquareFinder.Models
{
    [Table("PointList")]
    public class PointListEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PointEntity> Points { get; set; }

        public SquareEntity Square { get; set; }



        public PointListEntity()
        {
            Points = new List<PointEntity>();
        }

        public PointListEntity(string name)
        {
            Name = name;
            Points = new List<PointEntity>();
        }
    }
}