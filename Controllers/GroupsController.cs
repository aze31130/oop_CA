using Microsoft.AspNetCore.Mvc;
using oop_CA.Data;
using oop_CA.Models;
using System.Collections.Generic;
using System.Linq;

namespace oop_CA.Controllers
{
    public class GroupsController : Controller
    {
        private readonly Context _context;
        public GroupsController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Function to get the amount of student in a group
        public int getStudentAmount(int groupId)
        {
            int amount = 0;
            foreach (User user in _context.users.ToList())
            {
                if (user.groupId.Equals(groupId))
                {
                    amount++;
                }
            }
            return amount;
        }

        //Function to get a list of every student in a group
        public List<User> getStudentList(int groupId)
        {
            List<User> students = new List<User> { };
            foreach (User user in _context.users.ToList())
            {
                if (user.groupId.Equals(groupId))
                {
                    students.Add(user);
                }
            }
            return students;
        }
    }
}
