using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mdo.DB.Entities
{
    [Serializable]
    [Table("Cards")]
    public class CardEntity
    {
        [Key]
        public int CardId { get; set; }

        public string Name { get; set; }

        public string SpellType { get; set; }

        public string CardType { get; set; }

        public int RedMana { get; set; }
        public int BlueMana { get; set; }
        public int GreenMana { get; set; }
        public int WhiteMana { get; set; }
        public int BlackMana { get; set; }
        public int ColorlessMana { get; set; }

        public string Description { get; set; }

        public string Flavor { get; set; }

        public string Power { get; set; }

        public string Thoughnes { get; set; }

        public int ExpansionId { get; set; }

        [ForeignKey("ExpansionId")]
        public ExpansionEntity Expansion { get; set; }

        public string Rarity { get; set; }

        public string ImagePath { get; set; }
    }
}
