using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private Course getCourseById(int courseId)
        {
            return _context.courses.ToList().Find(x => x.id.Equals(courseId));
        }
        
        //Function to get all courses of a given student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> getGroupCourses(int groupId)
        {
            List<Course> studentCourses = new List<Course> { };
            foreach (Timetable tt in _context.timetables.ToList())
            {
                if (tt.groupId.Equals(groupId))
                {
                    studentCourses.Add(getCourseById(tt.courseId));
                }
            }
            return studentCourses;
        }

        //Overloading with a day filter
        public List<Course> getStudentCourses(int studentId, DAYS day)
        {
            throw new NotImplementedException();
        }
    }
}
