using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace JustDo_Web.Models
{
    public class ApplicationContext: IdentityDbContext<TodoUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoUser> TodoUsers { get; set; }
    }
}
