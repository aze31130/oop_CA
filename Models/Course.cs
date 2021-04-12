using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oop_CA.Models
{
    public enum DAYS
    {
        MONDAY, THUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY 
    }
    public class Course
    {
        public int id { get; set; }
        public DAYS day { get; set; }
        public int beginHour { get; set; }
        public int endHour { get; set; }
        public int teacherId { get; set; }
        public SUBJECTS subject { get; set; }
        public int groupId { get; set; }
    }
}
