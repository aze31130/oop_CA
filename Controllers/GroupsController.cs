using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System.Collections.Generic;
using System.Linq;
using static oop_CA.Utils.GroupsUtils;

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
            if (!isGroupValid(group))
            {
                return BadRequest(new { message = "Some course informations are incorrect !" });
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

        //Function to get the amount of student in a group
        public int getStudentAmount(int groupId)
        {
            int amount = 0;
            foreach (User user in _context.users.ToList())
            {
                /*
                if (user.groupId.Equals(groupId))
                {
                    amount++;
                }
                */
            }
            return amount;
        }

        //Function to get a list of every student in a group
        public List<User> getStudentList(int groupId)
        {
            List<User> students = new List<User> { };
            foreach (User user in _context.users.ToList())
            {
                /*
                if (user.groupId.Equals(groupId))
                {
                    students.Add(user);
                }
                */
            }
            return students;
        }
    }
}
