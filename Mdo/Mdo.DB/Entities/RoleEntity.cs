using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mdo.DB.Entities
{
    [Serializable]
    [Table("Roles")]
    public class RoleEntity 
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; } 
    }
}
