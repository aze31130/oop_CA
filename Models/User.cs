using System.ComponentModel.DataAnnotations;
using static oop_CA.Models.Enumeration;

namespace oop_CA.Models
{
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
        public USER_TYPE userType { get; set; }
        public string accessLevel { get; set; }
        public int amountToPay { get; set; }
        public int payedAmount { get; set; }
    }
}
