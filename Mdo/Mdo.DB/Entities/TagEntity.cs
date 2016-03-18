using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mdo.DB.Entities
{
    [Serializable]
    [Table("Tags")]
    public class TagEntity
    {
        [Key]
        public int TagId { get; set; }

        public string Name { get; set; }

        public  virtual ICollection<CardEntity> Cards { get; set; }
    }
}
