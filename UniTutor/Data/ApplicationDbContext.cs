//using Microsoft.EntityFrameworkCore;
//using UniTutor.Models;

//namespace UniTutor.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Tutor> Tutors { get; set; }
//        // Add DbSet properties for other entities
//        public DbSet<Student> Students { get; set; }

//        public DbSet<Comment> Comments { get; set; }

//        public DbSet<TutorRequest> TutorRequests { get; set; }
//    }
//}
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
        public DbSet<Student> Students { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TutorRequest> TutorRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tutor>()
                .Property(t => t.CreatedAt)
                .HasColumnType("datetime2"); // Ensure the type matches your MSSQL column type
        }
    }
}
