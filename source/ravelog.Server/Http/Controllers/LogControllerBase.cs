using RaveLog.Server.Http.Models;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RaveLog.Server.Http.Controllers
{
    public abstract class LogControllerBase : ApiController
    {
        protected IAsyncDocumentSession Session;

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            using (Session = Program.DocumentStore.OpenAsyncSession())
            {

                var response = await base.ExecuteAsync(controllerContext, cancellationToken);

                try
                {
                    response.EnsureSuccessStatusCode();
                    await Session.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.Write(e);
                }

                return response;
            }
        }

        public virtual async Task<HttpResponseMessage> Post([FromBody]LogEntry entry)
        {
            await Session.StoreAsync(entry);
            return Success();
        }
        
        public virtual async Task<HttpResponseMessage> Get(string id = null)
        {
            return Success(await Session.LoadAsync<LogEntry>(id));
        }

        protected HttpResponseMessage Success()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
        }

        protected HttpResponseMessage Success(LogEntry data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
        }

        protected HttpResponseMessage Success(IEnumerable<LogEntry> data)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
