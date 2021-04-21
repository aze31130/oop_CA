namespace oop_CA.Models
{
    public enum TYPE : int
    {
        WRITTEN = 1,
        ORAL = 2,
        HOMEWORK = 3,
        ONLINE = 4,
        FINAL_EXAM = 5
    }
    public enum STATE : int
    {
        INCOMING = 1,
        ENDED = 2
    }

    public class Exam
    {
        public int id { get; set; }
        public int groupId { get; set; }
        public int day { get; set; }
        public TYPE type { get; set; }
        public STATE state { get; set; }
    }
}
