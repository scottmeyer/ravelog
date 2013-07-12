using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RaveLog.Server.Http.Controllers
{
    public class HomeController: ApiController
    {
        private readonly string _index = File.ReadAllText("website\\index.html");
        
        public HttpResponseMessage Get()
        {
            var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(_index);
            response.Content.Headers.ContentType.MediaType = "text/html";
            return response;
        }

        public async Task<HttpResponseMessage> GetContent(string path)
        {
            var websitePath = String.Format("website\\{0}", path);

            try
            {
                var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new StringContent(await Task.Run(() => File.ReadAllText(String.Format(websitePath))));

                switch (Path.GetExtension(path).ToLower())
                {
                    case ".js":
                        response.Content.Headers.ContentType.MediaType = "text/javascript";
                        break;
                    case ".css":
                        response.Content.Headers.ContentType.MediaType = "text/css";
                        break;
                    case ".html":
                        response.Content.Headers.ContentType.MediaType = "text/html";
                        break;
                }

                return response;
            }
            catch (FileNotFoundException)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }


        }

        public async Task<HttpResponseMessage> GetImageContent(string path)
        {
            var websitePath = String.Format("website\\{0}", path);

            try
            {
                var response = ControllerContext.Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new ByteArrayContent(await Task.Run(() => File.ReadAllBytes(String.Format(websitePath))));
                response.Content.Headers.ContentType.MediaType = "image/png";

                return response;
            }
            catch (FileNotFoundException)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}