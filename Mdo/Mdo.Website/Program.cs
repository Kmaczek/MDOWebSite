using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace Mdo.Website
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = ConfigurationManager.AppSettings.Get("siteHostUrl");

            using (WebApp.Start<Startup>(new StartOptions(url)))
            {
                Console.WriteLine("WebApi Host started");
                Console.WriteLine("Hosting on: " + url);
                Console.ReadLine();
            }
        }
    }
}
