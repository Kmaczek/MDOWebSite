namespace Mdo.WebApi.Repos
{
    interface IUserRepository
    {
        bool Login(string user, string password);
    }
}
