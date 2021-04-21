using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;

namespace oop_CA.Controllers
{
    public class TimetablesController : Controller
    {
        private readonly Context _context;
        public TimetablesController(Context context)
        {
            _context = context;
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        //Overloading with a day filter
        public List<Course> getStudentCourses(int studentId, DAYS day)
        {
            throw new NotImplementedException();
        }
    }
}
