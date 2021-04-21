using Microsoft.AspNetCore.Mvc;
using oop_CA.Models;
using System;

namespace oop_CA.Controllers
{
    public class CoursesController : Controller
    {
        //Function to add a course to a student
        public int addCourse(Course course, int studentId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Remove()
        {
            return View();
        }
    }
}
