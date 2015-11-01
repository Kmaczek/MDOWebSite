using Mdo.Website.Security;

namespace Mdo.Website.Models
{
    public class LoginModel
    {
        public BearerToken Token { get; set; }

        public LoginModel()
        {
        }
    }
}