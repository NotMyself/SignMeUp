using System;
using NUnit.Framework;
using Nancy.Testing;

namespace SignMeUp.Tests.Helpers
{
    public abstract class SpecFor<T>
    {

        protected T subject;

        public abstract void Given();

        public abstract void When();

        [SetUp]
        public void SetUp()
        {
            Given();
            When();
        }
    }

    public abstract class with_a_browser : SpecFor<Browser>
    {
        private Action<ConfigurableBootstrapper.ConfigurableBoostrapperConfigurator> testSpecificConfiguration;

        protected void Configure(Action<ConfigurableBootstrapper.ConfigurableBoostrapperConfigurator> with)
        {
            testSpecificConfiguration = with;
        }

        public override void  Given()
        {
 	        subject = new Browser(new ConfigurableBootstrapper(testSpecificConfiguration));
        }

    }
}
