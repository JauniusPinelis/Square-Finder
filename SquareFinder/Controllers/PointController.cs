using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using SquareFinder.Db;
using SquareFinder.Models;

namespace SquareFinder.Controllers
{
    public class PointController : ApiController
    {
        private readonly IContext db = new Context();

        // GET: api/point/5
        [ResponseType(typeof(StateInformation))]
        [Route("api/Point/{pointListId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPoints([FromUri] int pointListId)
        {
            var data = new StateInformation(pointListId, db.PointLists);
            return Ok(data);
        }

        /// <summary>
        /// This is actually used to clear out points of specific pointList
        /// </summary>>
        [ResponseType(typeof(void))]
        [Route("api/Point/{id}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult PutPoint(int id)
        {
            var pointSet = db.PointLists.Find(id);
            if (pointSet == null)
            {
                return NotFound();
            }

            foreach (var point in pointSet.Points.ToList())
                db.Points.Remove(point);

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/point
        [AcceptVerbs("POST")]
        public IHttpActionResult PostPoint([FromBody] PointDataModel pointData)
        {
            StringBuilder errorBuilder = new StringBuilder();
            var point = new Point(pointData.X, pointData.Y);
            if (point.IsValid(db,pointData.PointListId, ref errorBuilder))
            {
                var pointList = db.PointLists.Find(pointData.PointListId);
                pointList?.Points.Add(point);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = point.Id }, point);
            }

            return Content(HttpStatusCode.BadRequest, errorBuilder.ToString());  
        }

        // DELETE: api/point/5
        [ResponseType(typeof(Point))]
        [Route("api/Point/{id}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeletePoint(int id)
        {

            Point point = db.Points.Find(id);
            if (point == null)
            {
                return NotFound();
            }

            db.Points.Remove(point);
            db.SaveChanges();

            return Ok(point);
        }

        protected override void Dispose(bool disposing)
        {
            if (db is IDisposable)
            {
                ((IDisposable)db).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}