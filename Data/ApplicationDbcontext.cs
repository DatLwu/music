using Microsoft.EntityFrameworkCore;
using demo.Models;
using Microsoft.Identity.Client;
namespace demo.Data{
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<songs> songs { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}