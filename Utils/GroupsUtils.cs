using oop_CA.Models;
using System.Collections.Generic;

namespace oop_CA.Utils
{
    public class GroupsUtils
    {
        //-----
        //Returns true if a group is valid
        //-----
        public static bool isGroupValid(Group group, List<User> allTeacher)
        {
            //Check if the id of the referent is a teacher
            foreach (User teacher in allTeacher)
            {
                if (group.referentTeacherId.Equals(teacher.id))
                {
                    return true;
                }
            }
            return false;
        }

        //-----
        //Returns true if a group is valid
        //-----
        public static bool isStudentGroupValid(StudentGroup studentGroup, List<User> allUsers, List<Group> allGroups)
        {
            bool firstRequirement = false;
            bool secondRequirement = false;
            //Check if the studentId is valid
            foreach (User user in allUsers)
            {
                if (studentGroup.studentId.Equals(user.id))
                {
                    firstRequirement = true;
                    break;
                }
            }

            //Check if the groupId is valid
            foreach (Group group in allGroups)
            {
                if(studentGroup.groupId.Equals(group.id))
                {
                    secondRequirement = true;
                    break;
                }
            }

            return (firstRequirement && secondRequirement);
        }

        //-----
        //Function to get the amount of student in a group
        //-----
        public int getStudentAmount(int groupId, List<StudentGroup> allStudentGroups)
        {
            int amount = 0;
            foreach (StudentGroup sg in allStudentGroups)
            {
                if (sg.groupId.Equals(groupId))
                {
                    amount++;
                }
            }
            return amount;
        }

        //-----
        //Function to get a list of every student in a group
        //-----
        public static List<User> getStudentList(int groupId, List<StudentGroup> allStudentGroups, List<User> allUsers)
        {
            List<User> students = new List<User> { };
            foreach (StudentGroup sg in allStudentGroups)
            {
                if (sg.groupId.Equals(groupId))
                {
                    students.Add(getUserById(sg.studentId, allUsers));
                }
            }
            return students;
        }

        //-----
        //Returns a user knowing his userId
        //Note: if the userId isn't in the list return null
        //-----
        public static User getUserById(int userId, List<User> allUsers)
        {
            foreach (User user in allUsers)
            {
                if (user.id.Equals(userId))
                {
                    return user;
                }
            }
            return null;
        }
    }
}
