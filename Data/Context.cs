using Microsoft.EntityFrameworkCore;
using oop_CA.Models;
using System;
using static oop_CA.Models.Enumeration;

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
        public DbSet<StudentGroup> studentgroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().Property(x => x.day).HasConversion(v => v.ToString(), v => (DAY)Enum.Parse(typeof(DAY), v));
            modelBuilder.Entity<Course>().Property(x => x.subject).HasConversion(v => v.ToString(), v => (SUBJECT)Enum.Parse(typeof(SUBJECT), v));
            modelBuilder.Entity<Course>().Property(x => x.type).HasConversion(v => v.ToString(), v => (TYPE)Enum.Parse(typeof(TYPE), v));
            modelBuilder.Entity<Mark>().Property(x => x.subject).HasConversion(v => v.ToString(),v => (SUBJECT)Enum.Parse(typeof(SUBJECT), v));
            modelBuilder.Entity<User>().Property(x => x.userType).HasConversion(v => v.ToString(), v => (USER_TYPE)Enum.Parse(typeof(USER_TYPE), v));
        }
    }
}
