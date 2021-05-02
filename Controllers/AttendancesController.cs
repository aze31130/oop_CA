using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace oop_CA.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly Context _context;
        public AttendancesController(Context context)
        {
            _context = context;
        }
        //Function to get the amount of missed class
        public int getStudentMissedClass(int studentId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = AccessLevel.TEACHER + "," + AccessLevel.ADMIN)]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccessLevel.TEACHER + "," + AccessLevel.ADMIN)]
        public async Task<IActionResult> registerAction([Bind("courseId,studentId,isPresent")] Attendance attendance)
        {
            /*
            //Check if the attendance is valid
            if (!isAttendanceValid(attendance, _context.attendances.ToList()))
            {
                return BadRequest(new { message = "The given mark is invalid !" });
            }

            if (ModelState.IsValid)
            {
                mark.date = DateTime.UtcNow;
                _context.marks.Add(mark);
                await _context.SaveChangesAsync();
            }
            */
            return RedirectToAction("Index", "Attendances");
        }

        private bool isAttendanceValid(Attendance attendance, List<User> allUsers, List<Course> allCourses)
        {
            return false;
        }
    }
}
