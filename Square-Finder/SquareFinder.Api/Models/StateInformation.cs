using System.Collections.Generic;
using System.Linq;

namespace SquareFinder.Models
{
    public class StateInformation
    {
        public IEnumerable<PointEntity> Points { get; set; }
        public IEnumerable<Square> Squares { get; set; }
        public IEnumerable<PointList> PointLists { get; set; }
    }
}