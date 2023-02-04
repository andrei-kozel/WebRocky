using Microsoft.EntityFrameworkCore;
using WebRocky.Models;

namespace WebRocky.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Task> Task { get; set; }
    }
}
