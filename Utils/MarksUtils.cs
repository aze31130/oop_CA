using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace oop_CA.Utils
{
    public class MarksUtils
    {
        public static bool isMarkValid(Mark mark, List<User> allUsers)
        {
            bool firstRequirement = false;
            bool secondRequirement = false;

            foreach (User user in allUsers)
            {
                if (mark.studentId.Equals(user.id))
                {
                    firstRequirement = true;
                }
            }

            if ((mark.value >= 0) && (mark.value <= 100) && (mark.coefficient <= 20) && (mark.coefficient > 0))
            {
                secondRequirement = true;
            }

            return (firstRequirement && secondRequirement);
        }
        //-----
        //Returns every student's marks
        //-----
        public static List<Mark> getMarks(int studentId, List<Mark> allMarks)
        {
            List<Mark> marks = new List<Mark> { };

            foreach (Mark mark in allMarks)
            {
                if (mark.studentId.Equals(studentId))
                {
                    marks.Add(mark);
                }
            }
            return marks;
        }

        //-----
        //Returns the average of a list of mark
        //-----
        public double getStudentAverage(int studentId, List<Mark> allMarks)
        {
            List<Mark> marks = getMarks(studentId, allMarks);
            int totalSum = 0;
            int coefSum = 0;
            foreach (Mark mark in marks)
            {
                totalSum += (mark.value * mark.coefficient);
                coefSum += mark.coefficient;
            }

            //Safety for a divide by 0 exception
            if (coefSum.Equals(0))
            {
                coefSum = 1;
            }
            return (totalSum / coefSum);
        }

        //-----
        //Overloading with the list of student's marks
        //-----
        public double getStudentAverage(List<Mark> studentMarks)
        {
            int totalSum = 0;
            int coefSum = 0;
            foreach (Mark mark in studentMarks)
            {
                totalSum += (mark.value * mark.coefficient);
                coefSum += mark.coefficient;
            }

            //Safety for a divide by 0 exception
            if (coefSum.Equals(0))
            {
                coefSum = 1;
            }
            return (totalSum / coefSum);
        }

        //-----
        //Returns the student's highest mark
        //Note: The default max mark is -1.
        //-----
        public int getStudentHighestMark(int studentId, List<Mark> allMarks)
        {
            List<Mark> marks = getMarks(studentId, allMarks);
            int maxMark = -1;
            foreach (Mark mark in marks)
            {
                if (maxMark < mark.value)
                {
                    maxMark = mark.value;
                }
            }
            return maxMark;
        }

        //-----
        //Overloading with a list of marks
        //Note: The default max mark is -1.
        //-----
        public int getStudentHighestMark(List<Mark> marks)
        {
            int maxMark = -1;
            foreach (Mark mark in marks)
            {
                if (maxMark < mark.value)
                {
                    maxMark = mark.value;
                }
            }
            return maxMark;
        }

        //-----
        //Returns the student's lowest mark
        //Note: The default min mark is -1.
        //-----
        public int getStudentLowestMark(int studentId, List<Mark> allMarks)
        {
            List<Mark> marks = getMarks(studentId, allMarks);
            int minMark = 100;
            if (marks.Count() > 0)
            {
                foreach (Mark mark in marks)
                {
                    if (minMark > mark.value)
                    {
                        minMark = mark.value;
                    }
                }
            }
            else
            {
                minMark = -1;
            }
            return minMark;
        }

        //-----
        //Overloading with a list of marks
        //Note: The default min mark is -1.
        //-----
        public int getStudentLowestMark(List<Mark> marks)
        {
            int minMark = 100;
            foreach (Mark mark in marks)
            {
                if (minMark > mark.value)
                {
                    minMark = mark.value;
                }
            }
            return minMark;
        }

        //Function to get the average mark of a group
        public int getGroupAverage(int groupId)
        {
            //Get a list of every studentId
            //Get the average for every
            //Calcul the global average
            throw new NotImplementedException();
        }
    }
}
