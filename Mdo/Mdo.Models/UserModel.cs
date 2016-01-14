using System.Workflow.Activities.Rules;
using Mdo.Core;
using Mdo.Models.Dtos;
using Mdo.Persistence.Entities;

namespace Mdo.Models
{
    public class UserModel
    {
        public string Username { get; }

        public string Email { get; }

        public string Password { get; }

        public UserModel(UserRegistrationDto registrationDto)
        {
            // RuleEngine engine = new RuleEngine();
            // https://msdn.microsoft.com/pl-pl/library/aa561216.aspx
            Username = registrationDto.Username;
            Email = registrationDto.Email;
            Password = MdoSecurity.CreateHashedPassword(registrationDto.Password);
        }

        public User ToUser()
        {
            return new User
            {
                Username = Username,
                Email = Email,
                Password = Password
            };
        }
    }
}
