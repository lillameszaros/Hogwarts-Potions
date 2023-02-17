using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; }
        public HouseType HouseType { get; set; }
        public PetType PetType { get; set; }
        
        public Room Room { get; set; }
    }
}
