using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using System.Linq;
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
            return View(getUserCourse(getUserId(), _context.timetables.ToList(), _context.courses.ToList(), _context.studentgroups.ToList()));
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
