using Nancy;
using Nancy.ModelBinding;
using SignMeUp.Core;

namespace SignMeUp.Web.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule(IUserRepository userRepository)
        {
            Get["/"] = x => View["index"];
            Post["/"] = x =>
                                  {
                                      var user = this.Bind<User>();
                                      userRepository.Save(user);

                                      return View["thankyou", user];
                                  };
        }
    }
}