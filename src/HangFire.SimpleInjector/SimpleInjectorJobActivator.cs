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
            if (jobType == null)
                throw new ArgumentNullException("jobType");
            return container.GetInstance(jobType);
        }

        public override JobActivatorScope BeginScope()
        {
            return new SimpleInjectorScope(container, container.BeginExecutionContextScope());
        }
    }

    class SimpleInjectorScope : JobActivatorScope
    {
        private readonly Container container;
        private readonly Scope scope;

        public SimpleInjectorScope(Container container, Scope scope)
        {
            this.container = container;
            this.scope = scope;
        }

        public override object Resolve(Type type)
        {
            return container.GetInstance(type);
        }

        public override void DisposeScope()
        {
            scope.Dispose();
        }
    }
}