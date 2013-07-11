using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace RaveLog.Server.Http.Integration.Castle
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this._container = container;
        }

        public object GetService(Type t)
        {
            return this._container.Kernel.HasComponent(t) ? this._container.Resolve(t) : null;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            return this._container.ResolveAll(t).Cast<object>().ToArray();
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(this, this._container.Release);
        }

        public void Dispose()
        {
        }
    }

    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IDependencyScope _scope;
        private readonly Action<object> _release;
        private readonly List<object> _instances;

        public WindsorDependencyScope(IDependencyScope scope, Action<object> release)
        {
            if (scope == null)
            {
                throw new ArgumentNullException("scope");
            }

            if (release == null)
            {
                throw new ArgumentNullException("release");
            }

            this._scope = scope;
            this._release = release;
            this._instances = new List<object>();
        }

        public object GetService(Type t)
        {
            object service = this._scope.GetService(t);
            this.AddToScope(service);

            return service;
        }

        public IEnumerable<object> GetServices(Type t)
        {
            var services = this._scope.GetServices(t);
            this.AddToScope(services);

            return services;
        }

        public void Dispose()
        {
            foreach (object instance in this._instances)
            {
                this._release(instance);
            }

            this._instances.Clear();
        }

        private void AddToScope(params object[] services)
        {
            if (services.Any())
            {
                this._instances.AddRange(services);
            }
        }
    }
}
