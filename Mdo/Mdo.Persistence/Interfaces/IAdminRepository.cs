namespace Mdo.Persistence.Interfaces
{
    public interface IAdminRepository
    {
        void ProcessImageLabel(string cardName, string existingImageUri, string destinationLabelUri, int yoffset);

        void FetchCardImage(string cardName, string url);
    }
}
