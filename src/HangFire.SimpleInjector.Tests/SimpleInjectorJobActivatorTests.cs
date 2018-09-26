using Xunit;

namespace Hangfire.SimpleInjector.Tests
{
    using global::SimpleInjector;

    using SimpleInjector;
    using System;
    [Collection("Tests")]
    public class SimpleInjectorJobActivatorTests
    {
        private Container container;

        public SimpleInjectorJobActivatorTests()
        {
            container = new Container();
        }

        [Fact]
        public void CtorThrowsAnExceptionWhenContainerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once UnusedVariable
                var activator = new SimpleInjectorJobActivator(null);
            });
        }

        [Fact]
        public void ActivateJobCallsSimpleInjector()
        {
            var theJob = new TestJob();
            container.RegisterInstance(theJob);
            var activator = new SimpleInjectorJobActivator(container);
            var result = activator.ActivateJob(typeof(TestJob));
            Assert.Equal(theJob, result);
        }

    }
}