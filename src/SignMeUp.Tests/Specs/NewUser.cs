using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Nancy;
using Nancy.Testing;
using Shouldly;
using SignMeUp.Core;
using SignMeUp.Core.Extensions;
using SignMeUp.Data;
using SignMeUp.Tests.Helpers;
using SignMeUp.Web.Modules;

namespace SignMeUp.Tests.Specs
{
    [TestFixture]
    public class when_signing_up_as_a_new_user : with_a_browser
    {
        private User user;
        private BrowserResponse response;
        private DataContext dataContext;
        private IEmailService emailService;

        public override void Given()
        {
            user = new User { FirstName = "Dirk", LastName = "Diggler", Email = "dirk@diggler.com" };
            
            Configure(with =>
                          {
                              with.Module<RootModule>();
                              with.Dependency<IEmailService>(emailService = Substitute.For<IEmailService>());
                              with.Dependency<DataContext>(dataContext = TestDataContextFactory.Build());
                          });
            base.Given();
        }

        public override void When()
        {
            response = subject.Post("/foo/", with =>
                        {
                            with.FormValue("FirstName", user.FirstName);
                            with.FormValue("LastName", user.LastName);
                            with.FormValue("Email", user.Email);
                        });
        }

        [Test]
        public void it_should_be_successful()
        {
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public void it_should_thank_the_user()
        {
            response.Body["body"].ShouldContain("Thank you, {0}".FormatWith(user.FirstName));
        }

        [Test]
        public void it_should_save_the_user()
        {
            dataContext.Users.Any(x => x.Email.Equals(user.Email)).ShouldBe(true);
        }

        [Test]
        public void it_should_send_the_user_a_validation_email()
        {
            emailService.Received().SendActivationEmail(Arg.Is<User>(arg => arg.Email == user.Email));
        }
    }
}
