using Microsoft.EntityFrameworkCore;
using UniTutor.Models;

namespace UniTutor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tutor> Tutors { get; set; }
        // Add DbSet properties for other entities
        public DbSet<Student> Students { get; set; }
    }
}
