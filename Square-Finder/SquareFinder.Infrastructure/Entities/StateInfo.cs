using System.Collections.Generic;
using System.Linq;

namespace SquareFinder.Infrastructure.Entities
{
    public class StateInfo
    {
        public IEnumerable<PointEntity> Points { get; set; }
        public IEnumerable<SquareEntity> Squares { get; set; }
        public IEnumerable<PointListEntity> PointLists { get; set; }
    }
}