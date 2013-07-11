using Castle.MicroKernel.Registration;
using Castle.Windsor;
using RaveLog.Server.Http.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using RaveLog.Server.Http.Integration.Castle;
using RaveLog.Server.Transports;

namespace RaveLog.Server.Http
{
    public class RaveLogHttpTransport : LogTransport
    {
        private static CancellationTokenSource _tokenSource;

        public override void Open()
        {
            _container = new WindsorContainer();
            _container.Register(
                Component.For<LogTransport>()
                    .Instance(this)
                    .LifestyleSingleton(),

                Types.FromThisAssembly()
                    .BasedOn<ApiController>()
                    .LifestyleTransient()
            );

            _tokenSource = Start();
        }

        public override void Close()
        {
            _tokenSource.Cancel();
        }

        private static IWindsorContainer _container;

        public static CancellationTokenSource Start()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var config = new HttpSelfHostConfiguration("http://localhost:8088");
            config.MessageHandlers.Add(new CorsHandler());
            config.DependencyResolver = new WindsorDependencyResolver(_container);
            RegisterRoutes(config.Routes);

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait(cancellationTokenSource.Token);
            return cancellationTokenSource;
        }

        private static void RegisterRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute("WebsiteIcon",
                "favicon.ico",
            new { controller = "Home", action = "GetImageContent" });

            routes.MapHttpRoute("Website",
                "",
                new { controller = "Home" });

            routes.MapHttpRoute("LogInformation",
                "log/information",
                new { controller = "InformationLog"});

            routes.MapHttpRoute("LogWarning",
                "log/warning",
                new { controller = "WarningLog"});

            routes.MapHttpRoute("LogError",
                "log/error",
                new { controller = "ErrorLog"});

            routes.MapHttpRoute("LogTrace",
                "log/trace",
                new { controller = "TraceLog"});

            routes.MapHttpRoute("LogDefault",
                "log/{type}/{id}",
                new { type = "information", id = RouteParameter.Optional });

            routes.MapHttpRoute("WebsiteImageContent",
            "img/{*path}",
            new { controller = "Home", action="GetImageContent" });

            routes.MapHttpRoute("WebsiteContent",
            "{*path}",
            new { controller = "Home", action="GetContent" });
        }
    }
}
