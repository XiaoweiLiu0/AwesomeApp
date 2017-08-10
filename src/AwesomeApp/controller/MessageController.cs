using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using AwesomeApp.service;

namespace AwesomeApp.controller
{
    public class MessageController : ApiController
    {
        readonly DependencyService service;

        public MessageController(DependencyService service)
        {
            this.service = service;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(service.SayHello(), Encoding.UTF8, "text/plain");

            return response;
        }
    }
}