using NSubstitute;
using NUnit.Framework;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Shouldly;
using TestingEntityFramework.Core;
using TestingEntityFramework.Core.Services;
using TestingEntityFramework.Web.Modules;

namespace TestingEntityFramework.Tests
{
    [TestFixture]
    public class RootModuleTests
    {
        private INancyBootstrapper bootStrapper;
        private IUserRepository userRepository;
        private Browser browser;

        [SetUp]
        public void SetUp()
        {
            bootStrapper = new ConfigurableBootstrapper(with =>
                                {
                                    with.Dependency<IUserRepository>(userRepository = Substitute.For<IUserRepository>());
                                    with.Dependency<UserSignUpService>(new UserSignUpService(userRepository));
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
