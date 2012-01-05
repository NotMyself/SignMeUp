using System;
using NSubstitute;
using NUnit.Framework;
using Nancy.Testing;
using SignMeUp.Core;
using SignMeUp.Data;

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
        protected BrowserResponse response;
        protected DataContext dataContext;
        protected IEmailService emailService;

        protected void Configure(Action<ConfigurableBootstrapper.ConfigurableBoostrapperConfigurator> with)
        {
            testSpecificConfiguration = with;
        }

        private void LocalConfigure(ConfigurableBootstrapper.ConfigurableBoostrapperConfigurator with)
        {
            with.Dependency<IEmailService>(emailService = Substitute.For<IEmailService>());
            with.Dependency<DataContext>(dataContext = TestDataContextFactory.Build());
            if (testSpecificConfiguration != null)
                testSpecificConfiguration(with);

        }

        public override void  Given()
        {
 	        subject = new Browser(new ConfigurableBootstrapper(LocalConfigure));
        }

    }
}
