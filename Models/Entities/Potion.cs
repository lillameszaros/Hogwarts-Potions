using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; }

        public Student Student { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public BrewingStatus BrewingStatus { get; set; } = BrewingStatus.Brew;

        public Recipe Recipe { get; set; }



        /*private void GetBrewingStatus()
        {
            if (Ingredients.Count < 5)
            {
                BrewingStatus = BrewingStatus.Brew;
            }
            else if()
        }*/
    }
}
