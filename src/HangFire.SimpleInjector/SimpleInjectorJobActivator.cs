using System;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Hangfire.SimpleInjector
{
    public class SimpleInjectorJobActivator : JobActivator
    {
        private readonly Container container;

        public SimpleInjectorJobActivator(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this.container = container;
        }

        public override object ActivateJob(Type jobType)
        {
            return container.GetInstance(jobType);
        }

        public override JobActivatorScope BeginScope()
        {
            return new SimpleInjectorScope(container);
        }
    }

    class SimpleInjectorScope : JobActivatorScope
    {
        private readonly Container _container;

        public SimpleInjectorScope(Container container)
        {
            _container = container;
            _container.BeginExecutionContextScope();
        }

        public override object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }

        public override void DisposeScope()
        {
            var scope = _container.GetCurrentExecutionContextScope();
            if (scope != null)
                scope.Dispose();
        }
    }
}