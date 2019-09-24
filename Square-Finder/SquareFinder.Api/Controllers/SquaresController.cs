using Microsoft.AspNetCore.Mvc;
using SquareFinder.Api.Db;
using SquareFinder.Api.Repositories;
using SquareFinder.Models;

namespace SquareFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquaresController : Controller
    {
        private IDbRepository _repository;

        public SquaresController(IDbRepository repository)
        {
            _repository = repository;
        }

        public IActionResult GetSquares( int pointListId)
        {
            var pointList = _repository.GetPointListById(pointListId);
            if (pointList == null)
                return NotFound();

            return Ok(SquareEntity.GetSquares(pointList.Points));
        }
    }
}
