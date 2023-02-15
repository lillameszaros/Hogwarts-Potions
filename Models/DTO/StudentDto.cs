using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.DTO
{
    public class StudentDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; }
    }
}
