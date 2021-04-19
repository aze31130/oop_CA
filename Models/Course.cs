namespace oop_CA.Models
{
    public enum DAYS
    {
        MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY
    }
    public class Course
    {
        public int id { get; set; }
        public int teacherId { get; set; }
        public SUBJECTS subject { get; set; }
        public DAYS day { get; set; }
        public int beginHour { get; set; }
        public int endHour { get; set; }

        //todo function to get the amount of student in a course

        //TODO function to get a list of every student id from a course
    }
}
