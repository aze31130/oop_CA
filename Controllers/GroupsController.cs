using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static oop_CA.Utils.GroupsUtils;
using static oop_CA.Utils.UsersUtils;

namespace oop_CA.Controllers
{
    public class GroupsController : Controller
    {
        private readonly Context _context;
        public GroupsController(Context context)
        {
            _context = context;
        }

        //-----
        //Index View
        //-----
        public IActionResult Index()
        {
            return View(_context.groups.ToList());
        }

        //-----
        //Add student to a group View
        //-----
        public IActionResult Add()
        {
            return View();
        }

        //-----
        //Action for Add View
        //-----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addAction([Bind("studentId,groupId")] StudentGroup studentGroup)
        {
            if (isStudentGroupValid(studentGroup, _context.users.ToList(), _context.groups.ToList()))
            {
                _context.studentgroups.Add(studentGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Groups");
            }
            return BadRequest(new { message = "The entered group is invalid !" });
        }
        
        //-----
        //Create group View
        //-----
        public IActionResult Create()
        {
            return View();
        }

        //-----
        //Create action group View
        //-----
        public IActionResult createAction([Bind("name,referentTeacherId")] Group group)
        {
            //Check if the group is valid
            if (!isGroupValid(group, getAllTeachers(_context.users.ToList())))
            {
                return BadRequest(new { message = "The teacher id is incorrect !" });
            }
            //Insert it into the database
            _context.groups.Add(group);
            _context.SaveChanges();
            return RedirectToAction("Index", "Groups");
        }

        //-----
        //Group details View
        //-----
        public IActionResult Details()
        {
            return View();
        }

        //-----
        //Edit group View
        //-----
        public IActionResult Edit()
        {
            return View();
        }

        //-----
        //Remove group View
        //-----
        public IActionResult Remove()
        {
            return View();
        }
    }
}
