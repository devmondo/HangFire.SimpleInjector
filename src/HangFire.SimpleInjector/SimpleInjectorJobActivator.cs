using System;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Hangfire.SimpleInjector
{
    public class SimpleInjectorJobActivator : JobActivator
    {
        private readonly Container _container;

        public SimpleInjectorJobActivator(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public override object ActivateJob(Type jobType)
        {
            return _container.GetInstance(jobType);
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            var scope = AsyncScopedLifestyle.BeginScope(_container);
            return new SimpleInjectorScope(_container, scope);
        }
    }

    internal class SimpleInjectorScope : JobActivatorScope
    {
        private readonly Container _container;
        private readonly Scope _scope;

        public SimpleInjectorScope(Container container, Scope scope)
        {
            _container = container;
            _scope = scope;
        }

        public override object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }

        public override void DisposeScope()
        {
            _scope.Dispose();
        }
    }
}
