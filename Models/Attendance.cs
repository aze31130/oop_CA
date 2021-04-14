namespace oop_CA.Models
{
    public class Attendance
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public int studentId { get; set; }
        public bool isPresent { get; set; }
    }
}
