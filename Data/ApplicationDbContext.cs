using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsEnrollment.Data.Entities;

namespace StudentsEnrollment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies().UseSqlServer("Server=localhost;Database=UniversityStudents;Trusted_Connection=True;MultipleActiveResultSets=true"); ;
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}