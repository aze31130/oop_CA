using Microsoft.EntityFrameworkCore;
using oop_CA.Models;
using System;

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
        public DbSet<Exam> exams { get; set; }
        public DbSet<StudentGroup> studentgroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().Property(x => x.type).HasConversion(v => v.ToString(), v => (TYPE)Enum.Parse(typeof(TYPE), v));
            modelBuilder.Entity<Exam>().Property(x => x.state).HasConversion(v => v.ToString(), v => (STATE)Enum.Parse(typeof(STATE), v));
            modelBuilder.Entity<Course>().Property(x => x.day).HasConversion(v => v.ToString(), v => (DAYS)Enum.Parse(typeof(DAYS), v));
            modelBuilder.Entity<Course>().Property(x => x.subject).HasConversion(v => v.ToString(), v => (SUBJECTS)Enum.Parse(typeof(SUBJECTS), v));
            modelBuilder.Entity<Mark>().Property(x => x.subject).HasConversion(v => v.ToString(),v => (SUBJECTS)Enum.Parse(typeof(SUBJECTS), v));
            modelBuilder.Entity<User>().Property(x => x.userType).HasConversion(v => v.ToString(), v => (USER_TYPE)Enum.Parse(typeof(USER_TYPE), v));
        }
    }
}
