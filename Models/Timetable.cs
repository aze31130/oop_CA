using System.ComponentModel.DataAnnotations;

namespace oop_CA.Models
{
    public class Timetable
    {
        [Key]
        public int id { get; set; }
        public int courseId { get; set; }
        public int groupId { get; set; }
        public int teacherId { get; set; }
    }
}
