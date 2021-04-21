using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oop_CA.Controllers
{
    public class UsersController : Controller
    {
        public IConfiguration Configuration;
        private readonly Context _context;

        public UsersController(Context context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Manage()
        {
            return View(GetUsers());
        }

        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = AccessLevel.ADMIN)]
        public IActionResult Update()
        {
            return View();
        }

        //[HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult authenticate([Bind("username,password")] AuthenticateModel model)
        {
            User user = AuthenticateUser(model.username, model.password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.username.ToString()),
                     new Claim(ClaimTypes.Role, user.accessLevel ?? "null")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.id,
                Username = user.username,
                FirstName = user.firstname,
                LastName = user.lastname,
                Token = tokenString
            });
        }

        private User AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            User user = _context.users.FirstOrDefault(x => x.username.Equals(username)) ?? null;

            // check if username exists
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
            [Bind("firstname,lastname,email,username,password,userType,amountToPay,payedAmount,groupId,accessLevel")] User user)
        {
            if (user.userType.Equals(USER_TYPE.ADMIN) || user.userType.Equals(USER_TYPE.TEACHER))
            {
                user.groupId = -1;
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

        public static string getSHA256Hash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string getRandomSalt(int lenght)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] saltBuilder = new char[lenght];
            Random random = new Random();
            for (int i = 0; i < saltBuilder.Length; i++)
            {
                saltBuilder[i] = chars[random.Next(chars.Length)];
            }

            var finalSalt = new String(saltBuilder);
            return finalSalt;
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
    }
}
