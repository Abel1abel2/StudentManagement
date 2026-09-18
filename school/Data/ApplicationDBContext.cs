using Microsoft.EntityFrameworkCore;
using school.Models;
using school.ViewModels;
namespace school.Data
{
    public class ApplicationDBContext:DbContext

      
    {

  
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Register> Registers { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
    }
}
