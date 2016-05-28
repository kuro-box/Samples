using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace MyWebApi.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Route]
        public IHttpActionResult Get()
        {
            return Ok("Helloworld");
        }

        [Route("stream")]
        public IHttpActionResult GetStream()
        {
            var buffer = Encoding.ASCII.GetBytes("Helloworld");
            var stream = new MemoryStream(buffer);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            return ResponseMessage(response);
        }

        [Route("{name}")]
        public IHttpActionResult Post(string name)
        {
            return Ok(string.Format("Hello {0}", name));
        }

        [Route("post/{name}")]
        public IHttpActionResult PostName(string name)
        {
            var msg = string.Format("Post: {0}", name);
            var buffer = Encoding.ASCII.GetBytes(msg);
            var stream = new MemoryStream(buffer);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            return Ok(response);
        }
    }
}
