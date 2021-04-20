﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using oop_CA.Data;
using oop_CA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace oop_CA.Controllers
{
    public class UsersController : Controller
    {
        private readonly Context _context;
        public UsersController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manage()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        //Post method to register a student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> registerUser([Bind("id,firstname,lastname,email,password,userType")] User user)
        {
            if (user.userType.Equals(USER_TYPE.ADMIN) || user.userType.Equals(USER_TYPE.TEACHER))
            {
                user.groupId = -1;
                user.amountToPay = 0;
                user.payedAmount = 0;
            }
            
            user.password = getSHA256Hash(user.password);

            if (ModelState.IsValid)
            {
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }
            return View("Index");
        }

        private string getSHA256Hash(string password)
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

        //Returns a list of every users
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.users.ToListAsync();
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
