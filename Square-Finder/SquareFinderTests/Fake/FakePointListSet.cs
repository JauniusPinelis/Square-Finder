using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareFinder.Models;

namespace SquareFinderTests.Fake
{
    public class FakePointListSet : FakeDbSet<PointList>
    {
        public override PointList Find(params object[] keyValues)
        {
            return this.SingleOrDefault(e => e.Id == (int)keyValues.Single());
        }
    }
}
