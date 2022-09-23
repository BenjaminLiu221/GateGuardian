using GateGuardianWeb.Models.Passwords;
using Microsoft.EntityFrameworkCore;

namespace GateGuardianWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Password> Passwords { get; set; }
    }
}
