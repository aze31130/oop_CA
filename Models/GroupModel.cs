using System.Collections.Generic;

namespace oop_CA.Models
{
    public class GroupModel : Group
    {
        public List<User> userList { get; set; }
    }
}
