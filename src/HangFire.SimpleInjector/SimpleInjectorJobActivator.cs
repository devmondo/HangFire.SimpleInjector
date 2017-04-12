using System;
using Injector = SimpleInjector;

namespace Hangfire.SimpleInjector
{
    public class SimpleInjectorJobActivator : JobActivator
    {
        private readonly Injector.Container _container;
        private readonly Injector.Lifestyle _lifestyle;

        public SimpleInjectorJobActivator(Injector.Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public SimpleInjectorJobActivator(Injector.Container container, Injector.Lifestyle lifestyle)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (lifestyle == null)
            {
                throw new ArgumentNullException("lifestyle");
            }

            _container = container;
            _lifestyle = lifestyle;
        }

        public override object ActivateJob(Type jobType)
        {
            return _container.GetInstance(jobType);
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            if (_lifestyle == null || _lifestyle != Injector.Lifestyle.Scoped)
            {
                return new SimpleInjectorScope(_container, Injector.Lifestyles.AsyncScopedLifestyle.BeginScope(_container));
            }
            return new SimpleInjectorScope(_container, new Injector.Lifestyles.AsyncScopedLifestyle().GetCurrentScope(_container));
        }
    }

    internal class SimpleInjectorScope : JobActivatorScope
    {
        private readonly Injector.Container _container;
        private readonly Injector.Scope _scope;

        public SimpleInjectorScope(Injector.Container container, Injector.Scope scope)
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
