using oop_CA.Models;
using System;
using System.Collections.Generic;

namespace oop_CA.Utils
{
    public class CoursesUtils
    {
        //-----
        //Returns true if the course is valid
        //-----
        public static bool isCourseOk(Course course)
        {
            bool isOk = true;
            if (course.beginHour < 0 || course.beginHour > 24)
            {
                isOk = false;
            }

            if (course.endHour < 0 || course.endHour > 24)
            {
                isOk = false;
            }
            return isOk;
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
        //Returns the student list in a given course
        //-----
        public static List<User> getStudentsFromCourse(int courseId, List<Timetable> allTimetables, List<StudentGroup> allGroups)
        {
            throw new NotImplementedException();
        }
    }
}
