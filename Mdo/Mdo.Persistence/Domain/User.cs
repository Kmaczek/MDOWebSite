using System.ComponentModel.DataAnnotations;

namespace Mdo.Persistence.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
