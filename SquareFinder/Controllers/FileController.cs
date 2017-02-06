using System.Linq;
using SquareFinder.Db;
using SquareFinder.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;

namespace SquareFinder.Controllers
{
    public class FileController : ApiController
    {

        [AcceptVerbs("POST")]
        public async Task<HttpResponseMessage> ImportPoints()
        {
            using (var db = new Context())
            {
                var streamProvider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                var currentListId = streamProvider.Contents[0].ReadAsStringAsync().Result;
                var fileData = streamProvider.Contents[1].ReadAsStringAsync().Result;

                string errors = Point.ImportPoints(fileData, currentListId, db);

                return Request.CreateResponse(HttpStatusCode.Created, errors);
            }
        }

        [AcceptVerbs("DELETE")]
        public async Task<HttpResponseMessage> DeletePoints()
        {
            using (var db = new Context())
            {
                var streamProvider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                var currentListId = streamProvider.Contents[0].ReadAsStringAsync().Result;
                var fileData = streamProvider.Contents[1].ReadAsStringAsync().Result;

                string errors = Point.DeletePoints(fileData, currentListId, db);

                return Request.CreateResponse(HttpStatusCode.Accepted, errors);
            }
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage ExportPoints(int id)
        {
            using (var db = new Context())
            {
                var stringBuilder = new StringBuilder();
                var pointList = db.PointLists.Find(id);
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
            }
        }

    }
}
