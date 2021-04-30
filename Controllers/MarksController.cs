using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using System;
using System.Linq;
using oop_CA.Models;
using static oop_CA.Utils.MarksUtils;
using static oop_CA.Models.Enumeration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Edit(int? id)
        {
            if ((id == null) || (_context.marks.Find(id) == null))
            {
                return NotFound();
            }
            ViewData["id"] = id;
            return View(_context.marks.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("studentId,subject,teacherComment,coefficient,value")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                //Check if the mark id exist
                if (!isMarkExist(id))
                {
                    return BadRequest(new { message = "The given mark id is invalid !" });
                }

                Mark editMark = _context.marks.ToList().Find(x => x.id.Equals(id));
                editMark.studentId = mark.studentId;
                editMark.subject = mark.subject;
                editMark.teacherComment = mark.teacherComment;
                editMark.coefficient = mark.coefficient;
                editMark.value = mark.value;
                _context.Entry(editMark).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Marks");
            }
            return View(mark);
        }

        public IActionResult Index()
        {
            if (isTeacher(getUserId()) || isAdmin(getUserId()))
            {
                ViewData["Teacher"] = "OK";
            }
            return View(getMarks(getUserId(), _context.marks.ToList()));
        }

        public IActionResult Remove(int? id)
        {
            if ((id == null) || (_context.marks.Find(id) == null))
            {
                return NotFound();
            }
            ViewData["id"] = id;
            return View(_context.marks.Find(id));
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFunction(int id)
        {
            Mark mark = await _context.marks.FindAsync(id);
            _context.marks.Remove(mark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        //-----
        //Returns true if the given mark exist
        //-----
        private bool isMarkExist(int markId)
        {
            return (_context.marks.ToList().FindAll(x => x.id.Equals(markId)).Count > 0);
        }
    }
}
