using System;
using Nancy;

namespace Mdo.Website
{
    public class HomeModule : NancyModule
    {
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
