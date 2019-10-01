using SquareFinder.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace SquareFinder.Infrastructure.Entities
{
    public class StateInfo
    {
        public IEnumerable<PointDto> Points { get; set; }
        public IEnumerable<SquareDto> Squares { get; set; }
        public IEnumerable<PointListDto> PointLists { get; set; }
    }
}