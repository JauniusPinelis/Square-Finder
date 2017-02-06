
namespace SquareFinder.Models
{
    public class PointDataModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PointListId { get; set; }

        public PointDataModel()
        {

        }

        public PointDataModel(int x, int y, int pointListId)
        {
            X = x;
            Y = y;
            PointListId = pointListId;
        }
    }
}