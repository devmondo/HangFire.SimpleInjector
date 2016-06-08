using System;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Hangfire.SimpleInjector
{
    public class SimpleInjectorJobActivator : JobActivator
    {
        private readonly Container _container;
        private readonly Lifestyle _lifestyle;

        public SimpleInjectorJobActivator(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public SimpleInjectorJobActivator(Container container, Lifestyle lifestyle)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
            _lifestyle = lifestyle;
        }

        public override object ActivateJob(Type jobType)
        {
            return _container.GetInstance(jobType);
        }

        public override JobActivatorScope BeginScope()
        {
            if (_lifestyle == null && _lifestyle != Lifestyle.Scoped)
            {
                return new SimpleInjectorScope(_container, _container.BeginExecutionContextScope());
            }

            return new SimpleInjectorScope(_container, _container.GetCurrentLifetimeScope());
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
            if (_scope != null)
            {
                _scope.Dispose();
            }
        }
    }
}