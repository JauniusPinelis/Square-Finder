using System.Linq;
using SquareFinder.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SquareFinder.Api.Repositories;

namespace SquareFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {
        private IDbRepository _repository { get; set; }
        public FileController(IDbRepository repository)
        {
            _repository = repository;
        }

        /*[AcceptVerbs("POST")]
        public async Task<IActionResult> ImportPoints()
        {
            var streamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var currentListId = streamProvider.Contents[0].ReadAsStringAsync().Result;
            var fileData = streamProvider.Contents[1].ReadAsStringAsync().Result;

            string errors = _repository.ImportPoints(fileData, currentListId);
            if (errors != "")
                return BadRequest();

            return Ok();
        }

        [AcceptVerbs("DELETE")]
        public async Task<HttpResponseMessage> DeletePoints()
        {
            var streamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var currentListId = streamProvider.Contents[0].ReadAsStringAsync().Result;
            var fileData = streamProvider.Contents[1].ReadAsStringAsync().Result;

            string errors = _repository.DeletePoints(fileData, currentListId, db);

            return Request.CreateResponse(HttpStatusCode.Accepted, errors);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage ExportPoints(int id)
        {
            var stringBuilder = new StringBuilder();
            var pointList = _repository.GetPointListById(id);

            if (pointList != null)
            {
                var points = pointList.Points.ToList();

                foreach (var point in points)
                    stringBuilder.AppendLine(point.X + " " + point.Y);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(stringBuilder.ToString())
                };

                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                //we used attachment to force download
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "export.txt";
                return result;
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Pointlist has not been found");
        }*/

    }
}
