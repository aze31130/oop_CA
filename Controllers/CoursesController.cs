using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System;
using static oop_CA.Utils.CoursesUtils;

namespace oop_CA.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Context _context;
        public CoursesController(Context context)
        {
            _context = context;
        }
        //Function to add a course to a student
        public int addCourse(Course course, int studentId)
        {
            throw new NotImplementedException();
        }

        public IActionResult scheduleAction([Bind("teacherId,subject,type,day,beginHour,endHour")] Course course)
        {
            //Check if the course is valid
            if (!isCourseOk(course))
            {
                return BadRequest(new { message = "Some course informations are incorrect !" });
            }
            //Insert it into the database
            _context.courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Courses");
        }

        public IActionResult Schedule()
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
