using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static oop_CA.Models.Enumeration;
using static oop_CA.Utils.UsersUtils;

namespace oop_CA.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;

        public UsersController(Context context)
        {
            _context = context;
        }

        //-----
        //Index View
        //-----
        public IActionResult Index()
        {
            return View();
        }

        //-----
        //View to list every teacher
        //-----
        public IActionResult Teachers()
        {
            return View(getAllTeachers(_context.users.ToList()));
        }

        //-----
        //View to list every student
        //-----
        public IActionResult Students()
        {
            return View(getUsersByType(USER_TYPE.STUDENT));
        }

        //-----
        //Login View
        //-----
        public IActionResult Login()
        {
            return View();
        }

        //-----
        //Manage users View
        //-----
        [Authorize(Roles = AccessLevel.ADMIN)]
        public IActionResult Manage()
        {
            return View(GetUsers());
        }

        //-----
        //Register user View
        //-----
        public IActionResult Register()
        {
            return View();
        }

        //-----
        //Self user update view
        //-----
        [Authorize]
        public IActionResult Update()
        {
            return View();
        }

        //-----
        //Logout View
        //-----
        [Authorize]
        public IActionResult Logout()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> LogoutUser()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [AllowAnonymous]
        public IActionResult authenticate([Bind("username,password")] AuthenticateModel model)
        {
            User user = AuthenticateUser(model.username, model.password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.id.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.accessLevel),
            };

            var claimsIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
            HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index", "Home");
        }

        private User AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = _context.users.FirstOrDefault(x => x.username.Equals(username)) ?? null;

            //Check if username exists
            if (user == null)
            {
                return null;
            }

            // Granting access if the hashed password in the database matches with the password(hashed in computeHash method) entered by user.
            if (!getSHA256Hash(getSHA256Hash(password + user.salt) + user.salt).Equals(user.password))
            {
                return null;
            }
            return user;
        }

        //Post method to register a student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> registerUser(
            [Bind("firstname,lastname,email,username,password,userType,amountToPay,payedAmount,accessLevel")] User user)
        {
            if (user.userType.Equals(USER_TYPE.ADMIN) || user.userType.Equals(USER_TYPE.TEACHER))
            {
                user.amountToPay = 0;
                user.payedAmount = 0;
            }
            user.salt = getRandomSalt(10);
            user.password = getSHA256Hash(getSHA256Hash(user.password + user.salt) + user.salt);

            if (ModelState.IsValid)
            {
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
            return View("Index");
        }

        //Returns a list of every users
        private List<User> GetUsers()
        {
            return _context.users.ToList();
        }

        //Returns a user by knowing his id
        public User getUserById(int userId)
        {
            return _context.users.ToList().Find(x => x.id.Equals(userId));
        }

        //Returns a list of every user of a given type
        public List<User> getUsersByType(USER_TYPE type)
        {
            return _context.users.ToList().FindAll(x => x.userType.Equals(type));
        }



        ///
        [HttpPut("id")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if ((!id.Equals(user.id)) || (!_context.users.Any(x => x.id.Equals(id))))
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUsers", new { id = user.id }, user);
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }
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
