using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.DTO
{
    public class PotionDto
    {

        public string Name { get; set; }

        public StudentDto StudentDto { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public BrewingStatus BrewingStatus { get; set; } = BrewingStatus.Brew;

    }
}
