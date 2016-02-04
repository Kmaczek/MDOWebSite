using System.Collections;
using System.Collections.Generic;
using System.Workflow.Activities.Rules;
using Mdo.Core;
using Mdo.Models.Dtos;

namespace Mdo.Models
{
    public class UserModel
    {
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        public string Secret { get; }
        public ICollection<string> Roles { get; } = new List<string>();

        public UserModel(UserRegistrationDto registrationDto)
        {
            // RuleEngine engine = new RuleEngine();
            // https://msdn.microsoft.com/pl-pl/library/aa561216.aspx
            Username = registrationDto.Username;
            Email = registrationDto.Email;
            Password = MdoSecurity.CreateHashedPassword(registrationDto.Password);
            Secret = MdoSecurity.CreateSecret();
        }

        public UserModel(
            string username, 
            string email, 
            string password, 
            string secret,
            IEnumerable<string> roles)
        {
            Username = username;
            Email = email;
            Password = password;
            Secret = secret;
            AddRoles(roles);
        }

        private void AddRoles(IEnumerable<string> roles)
        {
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    Roles.Add(role);
                }
            }
        }
    }
}
