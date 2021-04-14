using System.Collections.Generic;

namespace oop_CA.Models
{
    public enum DAYS
    {
        MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY
    }
    public class Timetable
    {
        public int id { get; set; }
        public int courseId { get; set; }
        public int studentId { get; set; }
        public DAYS day { get; set; }
        public int beginHour { get; set; }
        public int endHour { get; set; }
    }
}
