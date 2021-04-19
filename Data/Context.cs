using Microsoft.EntityFrameworkCore;
using oop_CA.Models;

namespace oop_CA.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Attendance> attendances { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Mark> marks { get; set; }
        public DbSet<Timetable> timetables { get; set; }
        public DbSet<User> users { get; set; }
    }
}
