using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareFinder.Models;

namespace SquareFinderTests.Fake
{
    public class FakePointSet : FakeDbSet<Point>
    {
        public override Point Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Id == (int)keyValues.Single());
        }
    }
}
