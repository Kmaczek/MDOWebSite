using System;
using System.Configuration;
using System.IO;
using Mdo.Common;
using Mdo.WebApi.Setup;
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
            EstablishFileStructure();

            using (WebApp.Start<Startup>(ApplicationInfo.Instance.HostUri))
            {
                logger.Info("Website Host started");
                logger.Info("Hosting on: " + ApplicationInfo.Instance.HostUri);
                Console.ReadLine();
            }
        }

        private static void SetConsole()
        {
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = 100;
            Console.BufferHeight = 1000;
        }

        private static void EstablishFileStructure()
        {
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, ApplicationInfo.Instance.CardSavePath)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory,
                    ApplicationInfo.Instance.CardSavePath));
            }

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, ApplicationInfo.Instance.LabelsPath)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory,
                    ApplicationInfo.Instance.LabelsPath));
            }

            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, ApplicationInfo.Instance.ExpansionsPath)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory,
                    ApplicationInfo.Instance.ExpansionsPath));
            }
        }
    }
}
