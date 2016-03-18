using System;
using System.Collections.Generic;
using System.IO;
using Mdo.Common;
using Mdo.DB.Entities;
using Mdo.Models.Dtos;
using Mdo.WebApi.Setup;

namespace Mdo.WebApi.Massage
{
    public static class PrepareCard
    {
        public static string ImageExtension = ".png";
        public static void WebPaths(CardEntity card)
        {
            var uri = new UriBuilder(ApplicationInfo.Instance.HostUri);
            uri.Path = ApplicationInfo.Instance.CardSaveDirectory + "/" + card.ImagePath;
            card.ImagePath = uri.Uri.ToString();

            uri.Path = ApplicationInfo.Instance.LabelsSaveDirecotry + "/" + card.LabelPath;
            card.LabelPath = uri.Uri.ToString();
        }

        public static void WebPaths(List<CardDto> cards)
        {
            foreach (var card in cards)
            {
                var uri = new UriBuilder(ApplicationInfo.Instance.HostUri);
                uri.Path = ApplicationInfo.Instance.CardSaveDirectory + "/" + card.ImagePath;
                card.ImagePath = uri.Uri.ToString();

                uri.Path = ApplicationInfo.Instance.LabelsSaveDirecotry + "/" + card.LabelPath;
                card.LabelPath = uri.Uri.ToString();
            }
        }

        public static string ImageDiskPath(CardEntity cardModel)
        {
            return Path.Combine(ApplicationInfo.Instance.CardSavePath, cardModel.Name + ImageExtension);
        }

        public static string LabelDiskPath(CardEntity cardModel)
        {
            return Path.Combine(ApplicationInfo.Instance.LabelsPath, "label_" + cardModel.Name.Replace(' ', '_') + ImageExtension);
        }
    }
}
