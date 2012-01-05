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
    public class when_visiting_the_root_of_the_site : with_a_browser
    {
        private BrowserResponse response;

        public override void Given()
        {
            Configure(with =>
            {
                with.Module<RootModule>();
                with.Dependency<IEmailService>(Substitute.For<IEmailService>());
                with.Dependency<DataContext>(TestDataContextFactory.Build());
            });

            base.Given();
        }
        
        public override void When()
        {
            response = subject.Get("/");
        }

        [Test]
        public void it_should_be_successful()
        {
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public void it_should_show_the_signup_form()
        {
            response.Body["form"].ShouldExistOnce()
                    .And.Attribute["action"].ShouldBe("/");
        }
    }

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
            response = subject.Post("/", with =>
                        {
                            with.HttpRequest();
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