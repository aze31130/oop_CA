using Microsoft.EntityFrameworkCore;
using oop_CA.Models;

namespace oop_CA.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<User> users { get; set; }
    }
}
