using System;
using System.Configuration;
using Microsoft.Owin.Hosting;
using NLog;

namespace Mdo.WebApi
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            SetConsole();
            var url = ConfigurationManager.AppSettings.Get("siteHostUrl");

            using (WebApp.Start<Startup>(url))
            {
                logger.Info("Website Host started");
                logger.Info("Hosting on: " + url);
                Console.ReadLine();
            }
        }

        private static void SetConsole()
        {
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = 100;
            Console.BufferHeight = 1000;
        }
    }
}
