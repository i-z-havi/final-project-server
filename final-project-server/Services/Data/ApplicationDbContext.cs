using final_project_server.Models.Politics;
using final_project_server.Models.Users;
using final_project_server.Models.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace final_project_server.Services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProjectPolicySQL> Policies { get; set; }

        public DbSet<UserSQL> Users { get; set; }

        public DbSet<ProjectPolicySign> PolicySigners { get; set; }

        public DbSet<PolicyDetails> PolicyDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
