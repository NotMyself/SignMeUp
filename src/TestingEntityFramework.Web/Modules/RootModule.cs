using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace TestingEntityFramework.Web.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = x => View["index"];
        }
    }
}