using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Linq;
using static oop_CA.Utils.CoursesUtils;
using static oop_CA.Utils.UsersUtils;

namespace oop_CA.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Context _context;
        public CoursesController(Context context)
        {
            _context = context;
        }

        //-----
        //Index View
        //-----
        public IActionResult Index()
        {
            return View(_context.courses.ToList());
        }

        //Function to add a course to a student
        public int addCourse(Course course, int studentId)
        {
            throw new NotImplementedException();
        }

        //-----
        //View to create a course
        //-----
        public IActionResult Schedule()
        {
            return View();
        }

        //-----
        //Action for view Schedule
        //-----
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

        //-----
        //View to assign a course to a group
        //-----
        public IActionResult Assign()
        {
            return View();
        }

        //----- isTimetableValid(Timetable timetable, List<Course> allCourses, List<Group> allGroups, List<User> allTeachers)
        //Action for view Assign
        //-----
        public IActionResult assignAction([Bind("courseId,groupId,teacherId")] Timetable timetable)
        {
            //Verification to make sure the course, group and teacher exist
            if (!isTimetableValid(timetable, _context.courses.ToList(), _context.groups.ToList(), getAllTeachers(_context.users.ToList())))
            {
                return BadRequest(new { message = "The group or course or teacher doesn't exist !" });
            }
            //Add the timetable to the database
            _context.timetables.Add(timetable);
            _context.SaveChanges();
            return RedirectToAction("Index", "Courses");
        }

        //-----
        //Details View
        //-----
        public IActionResult Details()
        {
            return View();
        }

        //-----
        //Edit View
        //-----
        public IActionResult Edit()
        {
            return View();
        }

        //-----
        //List View
        //-----
        public IActionResult List()
        {
            return View(_context.courses.ToList());
        }

        //-----
        //Remove View
        //-----
        public IActionResult Remove()
        {
            return View();
        }
    }
}
