using Microsoft.AspNetCore.Mvc;
using System;

namespace oop_CA.Controllers
{
    public class AttendancesController : Controller
    {
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
    }
}
