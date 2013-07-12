using RaveLog.Server.Http.Models;
using RaveLog.Server.Transports;
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
        private readonly LogTransport _transport;

        protected LogControllerBase(LogTransport transport)
        {
            _transport = transport;
        }
        protected IAsyncDocumentSession Session;

        public abstract Severity Severity { get; }

        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            using (Session = Program.DocumentStore.OpenAsyncSession())
            {

                return await base.ExecuteAsync(controllerContext, cancellationToken);

                //We're not currently doing any storing here.
                //try
                //{
                //    response.EnsureSuccessStatusCode();
                //    await Session.SaveChangesAsync();
                //}
                //catch (Exception e)
                //{
                //    System.Diagnostics.Trace.Write(e);
                //}

                //return response;
            }
        }

        public virtual async Task<HttpResponseMessage> Post([FromBody]LogEntry entry)
        {
            entry.Severity = Severity.ToString();
            await _transport.OnLogEntryReceived(this, entry);
            return Success();
        }
        
        public virtual async Task<HttpResponseMessage> Get(string id = null)
        {
            return id != null 
                ? Success(await Session.LoadAsync<LogEntry>(id)) 
                : Success(await Session.Query<LogEntry>().Where(x => x.Severity == Severity.ToString()).ToListAsync());
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
