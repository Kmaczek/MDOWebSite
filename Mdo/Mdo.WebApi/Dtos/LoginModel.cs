using Mdo.WebApi.Security;

namespace Mdo.WebApi.Dtos
{
    public class LoginModel
    {
        public BearerToken Token { get; set; }
        
    }
}