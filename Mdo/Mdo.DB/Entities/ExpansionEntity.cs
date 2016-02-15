using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mdo.DB.Entities
{
    [Serializable]
    [Table("Expansions")]
    public class ExpansionEntity
    {
        [Key]
        public int ExpansionId { get; set; }

        public string Name { get; set; }

        public DateTime Started { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<CardEntity> Cards { get; set; }
    }
}
