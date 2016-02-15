using System;

namespace Mdo.Models.Models
{
    public class ExpansionModel
    {
        public ExpansionModel(
            string name,
            string imagePath,
            DateTime started)
        {
            Name = name;
            ImagePath = imagePath;
            Started = started;
        }

        public string Name { get; set; }

        public DateTime Started { get; set; }

        public string ImagePath { get; set; }
    }
}
