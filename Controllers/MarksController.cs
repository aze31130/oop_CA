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
        public int getStudentAverage(int studentId)
        {
            throw new NotImplementedException();
        }

        //Overloading for a list of marks
        public int getStudentAverage(List<Mark> marks)
        {
            throw new NotImplementedException();
        }

        //Function to get the highest mark
        public int getStudentHighestMark(int studentId)
        {
            throw new NotImplementedException();
        }

        //Overloading for a list of marks
        public int getStudentHighestMark(List<Mark> marks)
        {
            throw new NotImplementedException();
        }

        //Function to get the lowest mark
        public int getStudentLowestMark(int studentId)
        {
            throw new NotImplementedException();
        }

        //Overloading for a list of marks
        public int getStudentLowestMark(List<Mark> marks)
        {
            throw new NotImplementedException();
        }

        //Function to get the average mark of a group
        public int getGroupAverage(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
