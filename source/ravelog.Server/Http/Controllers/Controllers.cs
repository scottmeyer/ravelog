using Raven.Client;
using RaveLog.Server.Http.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;
using System.Net;

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
    public class InformationLogController : LogControllerBase
    {
        public override async Task<HttpResponseMessage> Post(LogEntry entry)
        {
            entry.Severity = Severity.Information.ToString();
            return await base.Post(entry);
        }

        public override async Task<HttpResponseMessage> Get(string id = null)
        {
            if(id != null)
                return await base.Get(id);
            else
            {
                return Success(await Session.Query<LogEntry>().Where(x => x.Severity == Severity.Information.ToString()).ToListAsync());
            }
        }
    }

    public class WarningLogController : LogControllerBase
    {
        public override async Task<HttpResponseMessage> Post(LogEntry entry)
        {
            entry.Severity = Severity.Warning.ToString();
            return await base.Post(entry);
        }
    }
    public class ErrorLogController : LogControllerBase
    {
        public override async Task<HttpResponseMessage> Post(LogEntry entry)
        {
            entry.Severity = Severity.Error.ToString();
            return await base.Post(entry);
        }
    }

    public class TraceLogController : LogControllerBase
    {
        public override async Task<HttpResponseMessage> Post(LogEntry entry)
        {
            entry.Severity = Severity.Trace.ToString();
            return await base.Post(entry);
        }
    }
}
