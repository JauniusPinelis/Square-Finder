using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SquareFinder.Db;
using SquareFinder.Models;
using SquareFinder.Repositories;

namespace SquareFinder.Controllers
{
    public class PointListController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        /// <summary>
        /// Return all PointLists except for the very first which is Default
        /// Before we save points into new point list, we use the default 'invisible' one
        /// </summary>
        /// 
        public PointListController()
        {
            var context = new Context();
        }

        public IQueryable<PointList> GetPointLists()
        {
            using (var db = new Context())
            {
                return db.PointLists;
            }
            
        }

        // GET: api/PointLists/5
        [ResponseType(typeof(PointList))]
        public IHttpActionResult GetPointList(int id)
        {
            
            PointList pointList = unitOfWork.PointListsRepository.Find(id);
            if (pointList == null)
            {
                return NotFound();
            }

            return Ok(pointList);
            
        }

        // POST: api/PointLists
        /// <summary>
        /// without [FromBody] this gives 405 errors      
        /// </summary>
        [ResponseType(typeof(PointList))]
        [System.Web.Http.AcceptVerbs("POST")]
        public IHttpActionResult PostPointList([FromBody] PointList pointList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointList.Points == null)
                pointList.Points = new List<Point>();

            // Check if pointlists exists. If it does - overwrite
            var existingPointList = unitOfWork.GetContext().PointLists.SingleOrDefault(x => x.Name == pointList.Name);
            if (existingPointList != null)
                existingPointList.Points = pointList.Points;
            else
                unitOfWork.PointListsRepository.Add(pointList);
            unitOfWork.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = pointList.Id}, pointList);
            
        }

        // DELETE: api/PointLists/5
        [ResponseType(typeof(PointList))]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeletePointList(int id)
        {
            using (var db = new Context())
            {
                PointList pointList = db.PointLists.Find(id);
                if (pointList == null)
                {
                    return NotFound();
                }

                //Removing points
                foreach (var point in pointList.Points.ToList())
                    db.Points.Remove(point);
                //and then pointlist
                db.PointLists.Remove(pointList);
                db.SaveChanges();

                return Ok(pointList);
            }
        }
    }
}