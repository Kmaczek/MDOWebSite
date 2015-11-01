using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Mdo.Website
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings.Get("siteHostUrl");

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("WebApi Host started");
                Console.WriteLine("Hosting on: " + url);
                Console.ReadLine();
            }
        }
    }
}
