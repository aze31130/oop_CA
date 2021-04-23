using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using static oop_CA.Models.Enumeration;

namespace oop_CA.Utils
{
    public class UsersUtils
    {
        //-----
        //Calculates the SHA256 hash of a string
        //-----
        public static string getSHA256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //-----
        //Generates a random string
        //-----
        public static string getRandomSalt(int lenght)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] saltBuilder = new char[lenght];
            Random random = new Random();
            for (int i = 0; i < saltBuilder.Length; i++)
            {
                saltBuilder[i] = chars[random.Next(chars.Length)];
            }
            return new String(saltBuilder);
        }

        //-----
        //Returns a list of all teachers
        //-----
        public static List<User> getAllTeachers(List<User> allUsers)
        {
            return allUsers.FindAll(x => x.userType.Equals(USER_TYPE.TEACHER));
        }
    }
}
