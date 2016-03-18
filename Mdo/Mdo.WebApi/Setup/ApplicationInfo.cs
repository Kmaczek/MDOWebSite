using System;
using System.Configuration;
using System.IO;

namespace Mdo.WebApi.Setup
{
    public class ApplicationInfo
    {
        private ApplicationInfo()
        {
            HostUri = ConfigurationManager.AppSettings.Get("siteHostUrl");
            ImagesPath = Path.Combine(Environment.CurrentDirectory, ConfigurationManager.AppSettings.Get("imagesPath"));
            CardSavePath = Path.Combine(Environment.CurrentDirectory, ConfigurationManager.AppSettings.Get("cardSavePath"));
            LabelsPath = Path.Combine(Environment.CurrentDirectory, ConfigurationManager.AppSettings.Get("labelSavePath"));
            ExpansionsPath = Path.Combine(Environment.CurrentDirectory, ConfigurationManager.AppSettings.Get("expansionsSavePath"));

            CardSaveDirectory = ConfigurationManager.AppSettings.Get("cardSavePath");
            LabelsSaveDirecotry = ConfigurationManager.AppSettings.Get("labelSavePath");
            ExpansionSaveDirectory = ConfigurationManager.AppSettings.Get("expansionsSavePath");
        }

        public string HostUri { get; }

        public string ImagesPath { get; }

        public string CardSavePath { get; }

        public string CardSaveDirectory { get; }

        public string LabelsPath { get; }

        public string LabelsSaveDirecotry { get; }

        public string ExpansionsPath { get; }

        public string ExpansionSaveDirectory { get; }

        public static ApplicationInfo Instance { get; } = new ApplicationInfo();
    }
}
