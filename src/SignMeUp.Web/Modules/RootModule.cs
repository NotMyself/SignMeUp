using Nancy;
using Nancy.ModelBinding;
using SignMeUp.Core;
using SignMeUp.Core.Services;

namespace SignMeUp.Web.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule(UserSignUpService userSignUp)
        {
            Get["/"] = x => View["index"];
            Post["/"] = x =>
                                  {
                                      var user = this.Bind<User>();
                                      
                                      userSignUp.SignUp(user);

                                      return View["thankyou", user];
                                  };
        }
    }
}