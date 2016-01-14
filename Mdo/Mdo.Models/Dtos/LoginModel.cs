using Mdo.WebApi.Security;

namespace Mdo.Models.Dtos
{
    public class LoginModel
    {
        public BearerToken Token { get; set; }
    }
}