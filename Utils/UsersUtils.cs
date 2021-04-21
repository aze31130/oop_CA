using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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
        //Returns the course object when given it's id
        //-----
        public static Course getCourseById(int courseId, List<Course> allCourses)
        {
            return allCourses.Find(x => x.id.Equals(courseId));
        }

        //-----
        //Returns a list of every courses of a given group
        //-----
        public static List<Course> getGroupCourse(int groupId, List<Timetable> allTimetables, List<Course> allCourses)
        {
            List<Course> groupCourses = new List<Course> { };
            foreach (Timetable tt in allTimetables)
            {
                if (tt.groupId.Equals(groupId))
                {
                    groupCourses.Add(getCourseById(tt.courseId, allCourses));
                }
            }
            return groupCourses;
        }

        //-----
        //Returns a list of every courses of a given student
        //-----
        public static List<Course> getUserCourse(int userId, List<Timetable> allTimetables, List<Course> allCourses, List<StudentGroup> allGroups)
        {
            List<Course> studentCourses = new List<Course> { };
            foreach (StudentGroup group in allGroups)
            {
                if (group.studentId.Equals(userId))
                {
                    studentCourses.AddRange(getGroupCourse(group.id, allTimetables, allCourses));
                }
            }
            return studentCourses;
        }

        //-----
        //Returns
        //-----
    }
}
