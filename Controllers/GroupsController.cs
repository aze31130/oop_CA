using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Details(int? id)
        {
            if ((id == null) || (_context.groups.Find(id) == null))
            {
                return NotFound();
            }
            ViewData["id"] = id;
            Group group = _context.groups.Find(id);
            GroupModel model = new GroupModel();
            model.id = group.id;
            model.name = group.name;
            model.referentTeacherId = group.referentTeacherId;
            model.userList = getUserFromId(getStudentListFromGroup((int)id));
            return View(model);
        }

        //-----
        //Edit group View
        //-----
        public IActionResult Edit(int? id)
        {
            if ((id == null) || (_context.groups.Find(id) == null))
            {
                return NotFound();
            }
            ViewData["id"] = id;
            return View(_context.groups.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("name,referentTeacherId")] Group group)
        {
            if (ModelState.IsValid)
            {
                //Check if the mark id exist
                if (!isGroupExists(id))
                {
                    return BadRequest(new { message = "The given group id is invalid !" });
                }

                Group editGroup = _context.groups.ToList().Find(x => x.id.Equals(id));
                editGroup.name = group.name;
                editGroup.referentTeacherId = group.referentTeacherId;
                _context.Entry(editGroup).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Groups");
            }
            return View(group);
        }

        //-----
        //Returns true if a group exists
        //-----
        private bool isGroupExists(int groupId)
        {
            return (_context.groups.ToList().FindAll(x => x.id.Equals(groupId)).Count > 0);
        }

        //-----
        //Remove group View
        //-----
        public IActionResult Remove(int? id)
        {
            if ((id == null) || (_context.groups.Find(id) == null))
            {
                return NotFound();
            }
            ViewData["id"] = id;
            return View(_context.groups.Find(id));
        }

        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFunction(int id)
        {
            Group group = await _context.groups.FindAsync(id);
            _context.groups.Remove(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Groups");
        }


        private List<StudentGroup> getStudentListFromGroup(int groupId)
        {
            return _context.studentgroups.ToList().FindAll(x => x.groupId.Equals(groupId));
        }

        private List<User> getUserFromId(List<StudentGroup> sg)
        {
            List<User> users = new List<User> { };
            foreach (StudentGroup s in sg)
            {
                users.Add(_context.users.ToList().Find(x => x.id.Equals(s.studentId)));
            }
            return users;
        }
    }
}
