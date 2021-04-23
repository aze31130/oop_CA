using System.ComponentModel.DataAnnotations;
using static oop_CA.Models.Enumeration;

namespace oop_CA.Models
{
    public class Course
    {
        [Key]
        public int id { get; set; }
        public int teacherId { get; set; }
        public SUBJECT subject { get; set; }
        public TYPE type { get; set; }
        public DAY day { get; set; }
        public int beginHour { get; set; }
        public int endHour { get; set; }
    }
}
