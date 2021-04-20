using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace oop_CA.Controllers
{
    public class MarksController : Controller
    {
        private readonly Context _context;
        public MarksController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int studentId = 2;
            return View(getMarks(studentId));
        }

        public IActionResult Create()
        {
            return View();
        }

        //Function to get student's marks
        private List<Mark> getMarks(int studentId)
        {
            List<Mark> marks = new List<Mark> { };

            foreach (Mark mark in _context.marks.ToList())
            {
                if (mark.studentId.Equals(studentId))
                {
                    marks.Add(mark);
                }
            }
            return marks;
        }

        //Function to get average of a list of mark
        public double getStudentAverage(int studentId)
        {
            List<Mark> marks = getMarks(studentId);
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

        //Overloading for a list of marks
        public int getStudentAverage(List<Mark> marks)
        {
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

        //Function to get the highest mark
        public int getStudentHighestMark(int studentId)
        {
            List<Mark> marks = getMarks(studentId);
            int maxMark = 0;
            foreach (Mark mark in marks)
            {
                if (maxMark < mark.value)
                {
                    maxMark = mark.value;
                }
            }
            return maxMark;
        }

        //Overloading for a list of marks
        public int getStudentHighestMark(List<Mark> marks)
        {
            int maxMark = 0;
            foreach (Mark mark in marks)
            {
                if (maxMark < mark.value)
                {
                    maxMark = mark.value;
                }
            }
            return maxMark;
        }

        //Function to get the lowest mark
        public int getStudentLowestMark(int studentId)
        {
            List<Mark> marks = getMarks(studentId);
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

        //Overloading for a list of marks
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
