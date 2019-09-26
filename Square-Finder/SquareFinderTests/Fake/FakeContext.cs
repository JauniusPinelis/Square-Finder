using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SquareFinderTests.Fake
{
    public class FakeContext : DbContext
    {
        public IDbSet<Point> Points { get; set; }
        public IDbSet<PointList> PointLists { get; set; }

        /// <summary>
        /// Also creates a Default PointList
        /// </summary>
        public FakeContext()
        {
            Points = new FakePointSet();
            PointLists = new FakePointListSet {new PointList("Default")};
        }

        /// <summary>
        /// This is needed for FakeContext as EF automatically maps PointLists
        /// with Points but FakeContext cannot.
        /// This does not have any isValid checks so need to be lil careful
        /// not to add existing points and etc.
        /// </summary>
        public void SyncPoints()
        {
            foreach (var point in PointLists.SelectMany(list => list.Points))
            {
                Points.Add(point);
            }
        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
