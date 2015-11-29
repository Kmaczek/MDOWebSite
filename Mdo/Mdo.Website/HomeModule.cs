using System;
using Mdo.Website.Common;
using Nancy;
using ILogger = Microsoft.Owin.Logging.ILogger;

namespace Mdo.Website
{
    public class HomeModule : NancyModule
    {
        public static IMdoLogger Log = new ConsoleLogger();
        public HomeModule()
        {
            Get["/"] = x =>
            {
                return View["index.html"];
            };

            Get["/{sth*}"] = x =>
            {
                try
                {
                    string param = x.sth.Value;
                    if (AskedForStaticContent(param))
                    {
                        return GetStaticContent(param);
                    }
                }
                catch (Exception e)
                {
                    Log.Write("Error during handling static content:  \n" + e);
                    return Response.AsRedirect("/");
                }
                
                return View["index.html"];
            };
        }

        private static bool AskedForStaticContent(string param)
        {
            return param.Contains("app");
        }

        private dynamic GetStaticContent(string param)
        {
            var appIndex = param.IndexOf("app", StringComparison.InvariantCulture);
            var newPath = param.Remove(0, appIndex).Replace(@"/", @"\");
            return Response.AsFile(newPath);
        }
    }
}
