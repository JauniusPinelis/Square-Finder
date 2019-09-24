using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SquareFinder.Api.Repositories;
using SquareFinder.Models;

namespace SquareFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointListsController : Controller
    {
        private IDbRepository _repository;
        public PointListsController(IDbRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetPointLists()
        {
            return Ok(_repository.GetPointLists().ToList());
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPointList(int id)
        {
            
            PointListEntity pointList = _repository.GetPointListById(id);
            if (pointList == null)
            {
                return NotFound();
            }

            return Ok(pointList);
            
        }


        [HttpPost]
        public IActionResult PostPointList([FromBody] PointListEntity pointList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if pointlists exists. If it does - overwrite
            var storedPointList = _repository.GetPointListById(pointList.Id);
            if (storedPointList == null)
            {
                _repository.AddPointList(pointList);
            }
            else
            {
                _repository.OverwritePointList(pointList.Id, pointList);
            } 

            return CreatedAtRoute("DefaultApi", new {id = pointList.Id}, pointList);
            
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeletePointList(int id)
        {
            var pointList = _repository.GetPointListById(id);
            if (pointList == null)
            {
                return NotFound();
            }

            _repository.RemovePointList(id);

            return NoContent();
        }
    }
}