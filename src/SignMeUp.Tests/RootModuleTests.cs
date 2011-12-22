using NSubstitute;
using NUnit.Framework;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Shouldly;
using SignMeUp.Core;
using SignMeUp.Core.Services;
using SignMeUp.Web.Modules;

namespace SignMeUp.Tests
{
    [TestFixture]
    public class RootModuleTests
    {
        
        private IUserRepository userRepository;
        private IEmailService emailService; 
        private Browser browser;


        [SetUp]
        public void SetUp()
        {
          var bootStrapper = new ConfigurableBootstrapper(with =>
                                {
                                    with.Dependency<IUserRepository>(userRepository = Substitute.For<IUserRepository>());
                                    with.Dependency<IEmailService>(emailService = Substitute.For<IEmailService>());
                                    with.Module<RootModule>();
                                });
            browser = new Browser(bootStrapper);
        }

        [Test]
        public void Can_get_the_signup_page()
        {
            var response = browser.Get("/");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public void Can_sign_up_for_news_letter()
        {
            var user = new User {FirstName = "Dirk", LastName = "Diggler", Email = "dirk@diggler.com"};
            var response = browser.Post("/", with =>
                                                 {
                                                     with.HttpRequest();
                                                     with.FormValue("FirstName", user.FirstName);
                                                     with.FormValue("LastName", user.LastName);
                                                     with.FormValue("Email", user.Email);
                                                 });

            userRepository.Received().Save(Arg.Any<User>());
        }

    }
}
