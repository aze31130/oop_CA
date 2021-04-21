using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public USER_TYPE userType { get; set; } //Wont be longer used
        public string accessLevel { get; set; }
        public int amountToPay { get; set; }
        public int payedAmount { get; set; }
    }
}
