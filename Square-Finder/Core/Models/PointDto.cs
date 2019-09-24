
namespace SquareFinder.Models
{
    public class PointDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int PointListId { get; set; }

        public PointDto()
        {

        }

        public PointDto(int x, int y, int pointListId)
        {
            X = x;
            Y = y;
            PointListId = pointListId;
        }
    }
}