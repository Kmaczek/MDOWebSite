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
                return View["index.html"];
            };
        }
    }
}
