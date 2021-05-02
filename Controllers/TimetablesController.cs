using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System.Collections.Generic;
using System.Linq;
using static oop_CA.Models.Enumeration;
using static oop_CA.Utils.CoursesUtils;

namespace oop_CA.Controllers
{
    [Authorize]
    public class TimetablesController : Controller
    {
        private readonly Context _context;
        public TimetablesController(Context context)
        {
            _context = context;
        }

        //-----
        //Index view for timetable
        //-----
        public IActionResult Index()
        {
            CourseModel model = new CourseModel();
            List<Course> allCourses = getUserCourse(getUserId(), _context.timetables.ToList(), _context.courses.ToList(), _context.studentgroups.ToList());
            model.monday = filterCourses(allCourses, DAY.MONDAY);
            model.tuesday = filterCourses(allCourses, DAY.TUESDAY);
            model.wednesday = filterCourses(allCourses, DAY.WEDNESDAY);
            model.thursday = filterCourses(allCourses, DAY.THURSDAY);
            model.friday = filterCourses(allCourses, DAY.FRIDAY);
            model.saturday = filterCourses(allCourses, DAY.SATURDAY);
            model.sunday = filterCourses(allCourses, DAY.SUNDAY);
            return View(model);
        }

        //-----
        //Returns the id of the currently logged account
        //-----
        private int getUserId()
        {
            return int.Parse(User.Identity.Name);
        }
    }
}
