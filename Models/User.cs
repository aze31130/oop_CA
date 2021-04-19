﻿namespace oop_CA.Models
{
    public enum USER_TYPE
    {
        STUDENT, TEACHER, ADMIN
    }

    public class User
    {
        public int id { get; set; }
        //-1 for teachers and admin
        public int groupId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public USER_TYPE userType { get; set; }
        public int amounttopay { get; set; }
        public int payedamount { get; set; }
    }
}
