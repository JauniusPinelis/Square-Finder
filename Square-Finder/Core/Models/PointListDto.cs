using System;
using System.Collections.Generic;
using System.Text;

namespace SquareFinder.Core.Models
{
    public class PointListDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public IEnumerable<PointDto> Points { get; set; }
    }
}
