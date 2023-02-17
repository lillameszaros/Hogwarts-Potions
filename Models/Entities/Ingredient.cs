using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();

        public virtual ICollection<Potion> Potion { get; set; } = new HashSet<Potion>();
    }
}
