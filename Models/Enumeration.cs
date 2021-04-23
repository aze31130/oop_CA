namespace oop_CA.Models
{
    public class Enumeration
    {
        public enum USER_TYPE : int
        {
            STUDENT = 1,
            TEACHER = 2,
            ADMIN = 3
        }

        public enum SUBJECT : int
        {
            MATHS = 1,
            PHYSICS = 2,
            PROGRAMMING = 3,
            ENGLISH = 4,
            ELECTRONICS = 5
        }

        public enum DAY : int
        {
            MONDAY = 1,
            TUESDAY = 2,
            WEDNESDAY = 3,
            THURSDAY = 4,
            FRIDAY = 5,
            SATURDAY = 6,
            SUNDAY = 7
        }

        public enum TYPE : int
        {
            COURSE = 1,
            ONLINE_COURSE = 2,
            WRITTEN_EXAM = 3,
            ORAL_EXAM = 4,
            ONLINE_EXAM = 5,
            FINAL_EXAM = 6
        }
    }
}
