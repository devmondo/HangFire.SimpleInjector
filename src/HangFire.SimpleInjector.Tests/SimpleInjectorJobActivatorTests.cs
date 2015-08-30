using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hangfire.SimpleInjector.Tests
{
    using global::SimpleInjector;

    using SimpleInjector;
    using System;
    [TestClass]
    public class SimpleInjectorJobActivatorTests
    {
        private Container container;

        public SimpleInjectorJobActivatorTests()
        {
        }

        [TestInitialize]
        public void SetUp()
        {
            container = new Container();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorThrowsAnExceptionWhenContainerIsNull()
        {
            // ReSharper disable once UnusedVariable
            var activator = new SimpleInjectorJobActivator(null);
        }
        [TestMethod]
        public void ActivateJobCallsSimpleInjector()
        {
            var theJob = new TestJob();
            container.RegisterSingleton<TestJob>(theJob);
            var activator = new SimpleInjectorJobActivator(container);
            var result = activator.ActivateJob(typeof(TestJob));
            Assert.AreEqual(theJob, result);
        }

    }
}