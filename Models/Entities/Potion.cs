using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; } = "Potion in progress";

        public Student Student { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();

        public BrewingStatus BrewingStatus { get; set; } = BrewingStatus.Brew;

        public Recipe Recipe { get; set; }

    }
}
