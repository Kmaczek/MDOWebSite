using System.IO;
using Nancy;

namespace Mdo.Website.NancyConfig
{
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            var path = Path.Combine(System.Environment.CurrentDirectory, "..", ".." );
            return Path.GetFullPath(path);
        }
    }
}
