using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mdo.DB.Entities
{
    [Serializable]
    [Table("Types")]
    public class TypeEntity
    {
        [Key]
        public int TypeId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CardEntity> Cards { get; set; }
    }
}
