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
            if (getUserId() > 0)
            {
                ViewData["Logged"] = "OK";
            }
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
        //Edit View
        //-----
        [Authorize(Roles = AccessLevel.ADMIN)]
        public IActionResult Edit(int? id)
        {
            if ((id == null) || (_context.users.Find(id) == null))
            {
                return NotFound();
            }
            ViewData["id"] = id;
            User user = _context.users.Find(id);
            UserUpdateModel model = new UserUpdateModel();
            model.firstname = user.firstname;
            model.lastname = user.lastname;
            model.email = user.email;
            model.username = user.username;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AccessLevel.ADMIN)]
        public IActionResult Edit(int id, [Bind("firstname,lastname,email,username")] UserUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                //Check if the mark id exist
                if (getUserById(id) == null)
                {
                    return BadRequest(new { message = "The given user id is invalid !" });
                }

                if ((string.IsNullOrEmpty(model.firstname)) || (string.IsNullOrEmpty(model.lastname))
                    || (string.IsNullOrEmpty(model.email)) || (string.IsNullOrEmpty(model.username)))
                {
                    return BadRequest(new { message = "You need to provide every fields !" });
                }

                User editUser = _context.users.ToList().Find(x => x.id.Equals(id));
                editUser.firstname = model.firstname;
                editUser.lastname = model.lastname;
                editUser.email = model.email;
                editUser.username = model.username;
                _context.Entry(editUser).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Users");
            }
            return View(model);
        }


        //-----
        //Self user update view
        //-----
        [Authorize]
        public IActionResult Update()
        {
            User user = _context.users.ToList().Find(x => x.id.Equals(getUserId()));
            UserUpdateModel model = new UserUpdateModel();
            model.firstname = user.firstname;
            model.lastname = user.lastname;
            model.email = user.email;
            model.username = user.username;
            return View(model);
        }

        //-----
        //Change password method
        //-----
        public IActionResult updateFunction([Bind("firstname,lastname,email,username")] UserUpdateModel model)
        {
            User user = _context.users.ToList().Find(x => x.id.Equals(getUserId()));

            user.firstname = model.firstname;
            user.lastname = model.lastname;
            user.email = model.email;
            user.username = model.username;

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        //-----
        //Change password view
        //-----
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        //-----
        //Change password method
        //-----
        public IActionResult changePasswordFunction([Bind("oldPassword,newPassword,confirmedNewPassword")] ChangePasswordModel model)
        {
            User user = _context.users.ToList().Find(x => x.id.Equals(getUserId()));
            //Check if old password is valid
            if (AuthenticateUser(user.username, model.oldPassword) == null)
            {
                return BadRequest(new { message = "Your first password is incorrect" });
            }

            //Check if the new password and the confirmed password matches
            if (!model.newPassword.Equals(model.confirmedNewPassword))
            {
                return BadRequest(new { message = "The passwords doesn't match" });
            }

            user.password = getSHA256Hash(getSHA256Hash(model.newPassword + user.salt) + user.salt);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("Index", "Users");
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
            [Bind("firstname,lastname,email,username,password,userType,amountToPay,payedAmount")] User user)
        {
            if (isNameAlreadyTaken(user.username, _context.users.ToList()))
            {
                return BadRequest(new { message = "Username is already taken !" });
            }

            if (user.userType.Equals(USER_TYPE.ADMIN) || user.userType.Equals(USER_TYPE.TEACHER))
            {
                user.amountToPay = 0;
                user.payedAmount = 0;
            }
            user.salt = getRandomSalt(10);
            user.password = getSHA256Hash(getSHA256Hash(user.password + user.salt) + user.salt);

            switch (user.userType)
            {
                case USER_TYPE.ADMIN:
                    user.accessLevel = "ADMIN";
                    break;
                case USER_TYPE.TEACHER:
                    user.accessLevel = "TEACHER";
                    break;
                case USER_TYPE.STUDENT:
                    user.accessLevel = "STUDENT";
                    break;
                default:
                    user.accessLevel = "UNKNOWN";
                    break;
            }

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
    }
}
