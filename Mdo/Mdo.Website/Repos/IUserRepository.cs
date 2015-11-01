namespace Mdo.Website.Repos
{
    interface IUserRepository
    {
        bool Login(string user, string password);
    }
}
