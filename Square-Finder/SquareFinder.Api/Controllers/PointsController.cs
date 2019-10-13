using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquareFinder.Core.Models;
using SquareFinder.Infrastructure.Entities;
using SquareFinder.Infrastructure.Repositories;
namespace SquareFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : Controller
    {
        private IDbRepository _repository;
        private IMapper _mapper;
        public PointsController(IDbRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetPoints(int pointListId)
        {
            //TODO this is not restful need to fix this
            var data = _repository.GetStateInfo();
            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddPoint([FromBody] PointDto pointData)
        {
            var point = _mapper.Map<PointEntity>(pointData);
            var pointList = _repository.GetPointListById(pointData.PointListId);

            if (point.IsValid(pointList))
            {
                _repository.AddPoint(point);
                _repository.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = point.Id }, point);
            }

            _repository.AddPoint(point);

            return CreatedAtRoute("DefaultApi", new { id = point.Id }, point);  
        }

        [HttpDelete]
        public IActionResult DeletePoint([FromBody] int pointId)
        {

            PointEntity point = _repository.GetPointById(pointId);
            if (point == null)
            {
                return NotFound();
            }

            _repository.DeletePoint(point);
            _repository.SaveChanges();

            return Ok(point);
        }
    }
}