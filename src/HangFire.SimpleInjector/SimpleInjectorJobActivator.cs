using System;
using SimpleInjector;

namespace Hangfire.SimpleInjector
{
    public class SimpleInjectorJobActivator : JobActivator
    {
        private readonly Container container;

        public SimpleInjectorJobActivator(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public override object ActivateJob(Type jobType)
        {
            return container.GetInstance(jobType);
        }
    }
}
