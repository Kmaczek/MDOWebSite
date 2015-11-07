using Mdo.WebApi.Security;

namespace Mdo.WebApi.Models
{
    public class LoginModel
    {
        public BearerToken Token { get; set; }

        public LoginModel()
        {
        }
    }
}