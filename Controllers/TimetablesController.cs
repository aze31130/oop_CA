using Microsoft.AspNetCore.Mvc;
using oop_CA.Models;
using System;
using System.Collections.Generic;

namespace oop_CA.Controllers
{
    public class TimetablesController : ControllerBase
    {
        //Function to get all courses of a given student
        public List<Course> getStudentCourses(int studentId)
        {
            throw new NotImplementedException();
        }

        //Overloading with a day filter
        public List<Course> getStudentCourses(int studentId, DAYS day)
        {
            throw new NotImplementedException();
        }
    }
}
