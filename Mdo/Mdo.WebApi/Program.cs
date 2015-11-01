using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace Mdo.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings.Get("siteHostUrl");

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Website Host started");
                Console.WriteLine("Hosting on: " + url);
                System.Diagnostics.Process.Start(url);
                Console.ReadLine();
            }
        }
    }
}
