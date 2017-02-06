using Microsoft.VisualStudio.TestTools.UnitTesting;
using SquareFinder.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquareFinderTests.Fake;

namespace SquareFinderTests
{
    [TestClass]
    public class SquareFinderTests
    {
        private List<Square> squares;
        private List<Point> points;

        [TestInitialize]
        public void Initialize()
        {
            squares = new List<Square>();
            points = new List<Point>();
        }

        /// <summary>
        /// Check if no data case gives no errors
        /// </summary>
        [TestMethod]
        public void CheckNoErrorWithNoPoints()
        {
            squares = Square.GetSquares(points);
            Assert.AreEqual(0, squares.Count);
        }

        /// <summary>
        /// Test if the square count algortihm works with not enough points
        /// </summary>
        [TestMethod]
        public void CheckNoErrorWithOnly3Points()
        {
            points.Add(new Point(0, 1));
            points.Add(new Point(4, 1));
            points.Add(new Point(12, 1));
            squares = Square.GetSquares(points);
            Assert.AreEqual(0, squares.Count);

        }

        /// <summary>
        /// Test case where only 4 points are present and single square should be found
        /// </summary>
        [TestMethod]
        public void CheckIfSingleRectangleIsFound()
        {
            points.Add(new Point(2, 2));
            points.Add(new Point(4, 4));
            points.Add(new Point(2, 4));
            points.Add(new Point(4, 2));
            squares = Square.GetSquares(points);
            Assert.AreEqual(1, squares.Count);

        }

        /// <summary>
        /// A case with 8 points and only 2 square should be found
        /// </summary>
        [TestMethod]
        public void CheckIfTwoRectanglesAreFound()
        {
            points.Add(new Point(2, 2));
            points.Add(new Point(4, 4));
            points.Add(new Point(2, 4));
            points.Add(new Point(4, 2));

            points.Add(new Point(8, 6));
            points.Add(new Point(6, 8));
            points.Add(new Point(6, 6));
            points.Add(new Point(8, 8));
            squares = Square.GetSquares(points);
            Assert.AreEqual(2, squares.Count);
        }

        /// <summary>
        /// Test to check if points with negative coordinate give no errors
        /// </summary>
        [TestMethod]
        public void CheckIfSquareWithNegativePointsIsFound()
        {
            points.Add(new Point(-2, -2));
            points.Add(new Point(-4, -4));
            points.Add(new Point(-2, -4));
            points.Add(new Point(-4, -2));
            squares = Square.GetSquares(points);
            Assert.AreEqual(1, squares.Count);
        }

        /// <summary>
        /// Simple testy for data conversion - basic correct string with point coordinates dont give errors
        /// </summary>
        [TestMethod]
        public void CheckIfDataConvertWorksWithoutErrors()
        {
            var testData = "5 16\r\n9 16\r\n5 13\r\n9 13\r\n9 17\r\n5 10\r\n9 10\r\n";
            var errorBuilder = new StringBuilder();
            IEnumerable<Point> output = Point.ConvertDataToPoints(testData, ref errorBuilder);
            Assert.AreEqual("", errorBuilder.ToString());
        }

        /// <summary>
        /// Checking if custom build Fake context works as expected
        /// </summary>
        [TestMethod]
        public void CheckIfFakeContextWorks()
        {
            var context = new FakeContext();
            context.PointLists.Find(0).Points = new List<Point>
            {
                new Point(0, 2),
                new Point(2, 0),
                new Point(0, 0),
                new Point(2, 2)
            };

            Assert.AreEqual(4, context.PointLists.Find(0).Points.Count);
        }

        /// <summary>
        /// Checking Mass Delete Import method works as expected
        /// </summary>
        [TestMethod]
        public void CheckIfDataDeleteWorks()
        {
            var context = new FakeContext();
            context.PointLists.Find(0).Points = new List<Point>
            {
                new Point(0, 2),
                new Point(2, 0),
                new Point(0, 0),
                new Point(2, 2)
            };
            context.SyncPoints();

            var fileData = "0 2\r\n2 0\r\n0 0\r\n2 2\r\n";
            Point.DeletePoints(fileData, "0", context);
            Assert.AreEqual(0, context.Points.Count());
        }

        /// <summary>
        /// Testing a simple case on file import
        /// </summary>
        [TestMethod]
        public void CheckIfImportWorksOnSimpleCase()
        {
            var context = new FakeContext();

            var fileData = "0 2\r\n2 0\r\n0 0\r\n2 2\r\n";
            Point.ImportPoints(fileData, "0", context);
            context.SyncPoints();
            Assert.AreEqual(4, context.Points.Count());
        }

        /// <summary>
        /// Import with added dublicates as out of range points
        /// </summary>
        [TestMethod]
        public void CheckIfImportWorksWithIncorrectData()
        {
            var context = new FakeContext();

            var fileData = "0 2\r\n2 0\r\n0 0\r\n2 2\r\n2 2\r\n15000 1000\r\n";
            Point.ImportPoints(fileData, "0", context);
            context.SyncPoints();
            Assert.AreEqual(4, context.PointLists.Find(0).Points.Count());
        }

        /// <summary>
        /// Input with about 12000 points - pointList should only allow 10000
        /// </summary>
        [TestMethod]
        public void CheckIfMaxSizeCannotBeExceeded()
        {
            // Building the string
            var stringBuilder = new StringBuilder();
            for (int i = -500; i <= 500; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    stringBuilder.AppendLine($"{i} {j}");
                }
            }

            var data = stringBuilder.ToString();

            var context = new FakeContext();

            Point.ImportPoints(data, "0", context);
            context.SyncPoints();
            Assert.AreEqual(10000, context.Points.Count());
        }

        /// <summary>
        /// Load test for finding squares
        /// Mainly to find out how long does it take to import andcalculate squares for 10000 points
        /// The results are sad.. It takes too long to process 10000 points and calculates squares
        /// 
        /// </summary>
        [TestMethod, Ignore]
        public void HeavyLoadTestForImportingAndCountingSquares()
        {
            // Building the string
            var stringBuilder = new StringBuilder();
            for (int i = -500; i <= 500; i++)
            {
                for (int j = 0; j <= 10; j++)
                {
                    stringBuilder.AppendLine($"{i} {j}");
                }
            }

            var data = stringBuilder.ToString();

            var context = new FakeContext();

            Point.ImportPoints(data, "0", context);
            context.SyncPoints();
            Assert.AreEqual(10000, Square.GetSquares(context.PointLists.Find(0).Points));
        }

        public void CheckIfPointListsSeparatePoints()
        {
            var data = "0 2\r\n2 0\r\n0 0\r\n2 2\r\n2 2\r\n1000 1000\r\n";

            var context = new FakeContext();
            context.PointLists.Add(new PointList("Test"));

            Point.ImportPoints(data, "1", context);
            Assert.AreEqual(0,context.PointLists.Find(0).Points.Count);
            Assert.AreEqual(4, context.PointLists.Find(1).Points.Count);
        }
    }
}
