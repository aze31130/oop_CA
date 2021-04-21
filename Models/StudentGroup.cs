using System.ComponentModel.DataAnnotations;

namespace oop_CA.Models
{
    public class StudentGroup
    {
        [Key]
        public int id { get; set; }
        public int studentId { get; set; }
        public int groupId { get; set; }
    }
}
