namespace oop_CA.Models
{
    public enum ADMIN_LEVEL
    {
        STUDENT, TEACHER, ADMIN
    }

    public class User
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public ADMIN_LEVEL adminlevel { get; set; }
        public int amounttopay { get; set; }
        public int payedamount { get; set; }
        public int groupId { get; set; }
    }
}
