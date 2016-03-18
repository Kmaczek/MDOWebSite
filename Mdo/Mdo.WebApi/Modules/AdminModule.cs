using System;
using System.IO;
using Mdo.Models.Dtos;
using Mdo.Persistence.Interfaces;
using Mdo.WebApi.Massage;
using Nancy;
using Nancy.ModelBinding;
using NLog;

namespace Mdo.WebApi.Modules
{
    public class AdminModule : NancyModule
    {
        private readonly IAdminRepository adminRepository;
        private readonly ICardsRepository cardsRepository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AdminModule(IAdminRepository adminRepo, ICardsRepository cardsRepository) : base("/admin")
        {
            this.adminRepository = adminRepo;
            this.cardsRepository = cardsRepository;

            FixCardLabel();
            DownloadCardImage();
        }

        public void FixCardLabel()
        {
            Post["/fixlabel"] = o =>
            {
                try
                {
                    logger.Info("GET /admin/fixlabel invoked");
                    var model = this.Bind<FixLabelDto>();
                    var card = cardsRepository.GetCard(model.CardName);
                    var imagePath = PrepareCard.ImageDiskPath(card);
                    var labelPath = PrepareCard.LabelDiskPath(card);

                    adminRepository.ProcessImageLabel(model.CardName, imagePath, labelPath, model.Yoffset);
                    return Response.AsJson(new { Message = "New label created." });
                }
                catch (Exception e)
                {
                    logger.Error(e, "Cannot fix label. Server error {0}", e.Message);
                    return Response.AsJson(new { Message = "Cannot fetch cards. Server error." },
                        HttpStatusCode.InternalServerError);
                }
            };
        }

        public void DownloadCardImage()
        {
            Post["/downloadimage"] = o =>
            {
                try
                {
                    logger.Info("GET /admin/fixlabel invoked");
                    var model = this.Bind<FetchImageDto>();

                    adminRepository.FetchCardImage(model.CardName, model.Url);
                    return Response.AsJson(new {Message = "Image downloaded and saved"});
                }
                catch (Exception e)
                {
                    logger.Error(e, "Cannot download image. Server error {0}", e.Message);
                    return Response.AsJson(new {Message = "Cannot download image. Server error." },
                        HttpStatusCode.InternalServerError);
                }
            };
        }
    }
}
