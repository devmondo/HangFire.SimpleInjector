using Hangfire.Annotations;
using SimpleInjector;
using System;

namespace Hangfire.SimpleInjector
{
    public static class GlobalConfigurationExtensions
    {
        public static IGlobalConfiguration<SimpleInjectorJobActivator> UseSimpleInjectorActivator([NotNull] this IGlobalConfiguration configuration,
            [NotNull] Container container)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return configuration.UseActivator(new SimpleInjectorJobActivator(container));
        }
    }
}
