using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Entities
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public int Capacity { get; set; }

        public ICollection<Student> Residents { get; set; } = new HashSet<Student>();
    }
}
