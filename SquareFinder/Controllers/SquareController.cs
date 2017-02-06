using SquareFinder.Db;
using SquareFinder.Models;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace SquareFinder.Controllers
{
    public class SquareController : ApiController
    {

        [ResponseType(typeof(Square))]
        [HttpGet]
        [Route("api/Square/{pointListId}")]
        public IHttpActionResult GetSquares([FromUri] int pointListId)
        {
            using (var db = new Context())
            {
                var pointList = db.PointLists.Find(pointListId);
                if (pointList != null)
                    return Ok(Square.GetSquares(pointList.Points));
                return Content(HttpStatusCode.BadRequest, "Pointlist has not been found");
            }
        }
    }
}
