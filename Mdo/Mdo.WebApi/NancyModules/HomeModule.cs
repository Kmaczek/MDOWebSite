using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Mdo.WebApi.NancyModules
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
