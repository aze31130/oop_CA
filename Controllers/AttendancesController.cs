using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //Check if the attendance is valid
            if (!isAttendanceValid(attendance, _context.users.ToList(), _context.courses.ToList()))
            {
                return BadRequest(new { message = "The given attendance is invalid ! Check the course / student Id" });
            }

            if (ModelState.IsValid)
            {
                _context.attendances.Add(attendance);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Index", "Attendances");
        }

        private bool isAttendanceValid(Attendance attendance, List<User> allUsers, List<Course> allCourses)
        {
            bool firstRequirement = false;
            bool secondRequirement = false;

            foreach (User u in allUsers)
            {
                if (attendance.studentId.Equals(u.id))
                {
                    firstRequirement = true;
                }
            }

            foreach (Course c in allCourses)
            {
                if (attendance.studentId.Equals(c.id))
                {
                    secondRequirement = true;
                }
            }

            return (firstRequirement && secondRequirement);
        }
    }
}
