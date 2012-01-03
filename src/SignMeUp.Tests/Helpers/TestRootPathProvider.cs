using System.IO;
using System.Reflection;
using Nancy;

namespace SignMeUp.Tests.Helpers
{
    public class TestRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);;
        }
    }
}
