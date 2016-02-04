using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Mdo.Models;

namespace Mdo.DB.Entities
{
    [Serializable]
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Secret { get; set; }

        public virtual ICollection<RoleEntity> Roles { get; set; }

        public UserEntity()
        {
            
        }

//        public UserEntity(UserModel userModel)
//        {
//            Username = userModel.Username;
//            Email = userModel.Email;
//            Password = userModel.Password;
//        }
//
//        public static implicit operator UserModel(UserEntity dbUser)
//        {
//            return new UserModel(dbUser.Username, dbUser.Email, dbUser.Password, dbUser.Roles.Select(x => x.Name));
//        }
    }
}
