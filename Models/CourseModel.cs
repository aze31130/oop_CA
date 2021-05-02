using System.Collections.Generic;

namespace oop_CA.Models
{
    public class CourseModel
    {
        public List<Course> monday { get; set; }
        public List<Course> tuesday { get; set; }
        public List<Course> wednesday { get; set; }
        public List<Course> thursday { get; set; }
        public List<Course> friday { get; set; }
        public List<Course> saturday { get; set; }
        public List<Course> sunday { get; set; }
    }
}
