using SquareFinder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquareFinder.Core.Models
{
    public class SquareDto
    {
        public int Id { get; set; }
        public List<PointDto> Points { get; set; }
    }
}
