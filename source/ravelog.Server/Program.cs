using RaveLog.Server.Http;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.ServiceConfigurators;

namespace RaveLog.Server
{
    class Program
    {
        public static EmbeddableDocumentStore DocumentStore;
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
                x.Service((ServiceConfigurator<RaveLogServiceControl> s)=>
                {
                    s.ConstructUsing(()=> new RaveLogServiceControl());
                    s.WhenStarted((sc, hc) => sc.Start(hc));
                    s.WhenStopped((sc, hc) => sc.Stop(hc));
            }));
        }
    }

    public class RaveLogServiceControl:ServiceControl
    {
        private CancellationTokenSource _cancellationTokenSource;
        public bool Start(HostControl hostControl)
        {
            Program.DocumentStore = new EmbeddableDocumentStore
            {
                UseEmbeddedHttpServer = true,
                RunInMemory = true,
            };

            Program.DocumentStore.Initialize();

            _cancellationTokenSource = RaveLogHttpTransport.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _cancellationTokenSource.Cancel();
            return true;
        }

    }
}
