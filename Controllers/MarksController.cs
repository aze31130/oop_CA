using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using System;
using System.Linq;
using oop_CA.Models;
using static oop_CA.Utils.MarksUtils;
using static oop_CA.Models.Enumeration;
using System.Threading.Tasks;

namespace oop_CA.Controllers
{
    [Authorize]
    public class MarksController : Controller
    {
        private readonly Context _context;
        public MarksController(Context context)
        {
            _context = context;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addAction([Bind("studentId,subject,teacherComment,coefficient,value")] Mark mark)
        {
            //Check if the mark is valid
            if (!isMarkValid(mark, _context.users.ToList()))
            {
                return BadRequest(new { message = "The given mark is invalid !" });
            }

            if (ModelState.IsValid)
            {
                _context.marks.Add(mark);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Marks");
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
            if (isTeacher(getUserId()) || isAdmin(getUserId()))
            {
                ViewData["Teacher"] = "OK";
            }
            return View(getMarks(getUserId(), _context.marks.ToList()));
        }

        public IActionResult Remove()
        {
            return View();
        }

        //-----
        //Returns the id of the currently logged account
        //Returns -1 if no one is logged
        //-----
        private int getUserId()
        {
            try
            {
                return int.Parse(User.Identity.Name);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //-----
        //Returns true if the given user is a teacher
        //-----
        private bool isTeacher(int userId)
        {
            return _context.users.ToList().Find(x => x.id.Equals(userId)).userType.Equals(USER_TYPE.TEACHER);
        }

        //-----
        //Returns true if the given user is an admin
        //-----
        private bool isAdmin(int userId)
        {
            return _context.users.ToList().Find(x => x.id.Equals(userId)).userType.Equals(USER_TYPE.ADMIN);
        }
    }
}
