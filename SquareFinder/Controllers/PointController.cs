using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using SquareFinder.Db;
using SquareFinder.Models;
using SquareFinder.Repositories;

namespace SquareFinder.Controllers
{
    public class PointController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        // GET: api/point/5
        [ResponseType(typeof(StateInformation))]
        [Route("api/Point/{pointListId}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetPoints([FromUri] int pointListId)
        {
            var data = new StateInformation(pointListId, unitOfWork.PointListsRepository.GetAll());
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
            var pointSet = unitOfWork.PointListsRepository.Find(id);
            if (pointSet == null)
            {
                return NotFound();
            }

            foreach (var point in pointSet.Points.ToList())
                unitOfWork.PointsRepository.Remove(point);

            unitOfWork.PointsRepository.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/point
        [AcceptVerbs("POST")]
        public IHttpActionResult PostPoint([FromBody] PointDataModel pointData)
        {
            StringBuilder errorBuilder = new StringBuilder();
            var point = new Point(pointData.X, pointData.Y);
            if (point.IsValid(unitOfWork.PointsRepository.GetDb(),pointData.PointListId, ref errorBuilder))
            {
                var pointList = unitOfWork.PointListsRepository.Find(pointData.PointListId);
                pointList?.Points.Add(point);
                unitOfWork.SaveChanges();
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

            Point point = unitOfWork.PointsRepository.Find(id);
            if (point == null)
            {
                return NotFound();
            }

            unitOfWork.PointsRepository.Remove(point);
            unitOfWork.PointsRepository.SaveChanges();

            return Ok(point);
        }
    }
}