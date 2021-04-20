namespace oop_CA.Models
{
    public enum USER_TYPE : int
    {
        STUDENT = 1,
        TEACHER = 2,
        ADMIN = 3
    }

    public class User
    {
        public int id { get; set; }
        public int groupId { get; set; } //-1 for teachers and admin
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public USER_TYPE userType { get; set; } //Wont be longer used
        public string accessLevel { get; set; }
        public int amountToPay { get; set; }
        public int payedAmount { get; set; }
    }
}
