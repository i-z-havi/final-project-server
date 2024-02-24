using final_project_server.Models.Politics;
using final_project_server.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace final_project_server.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<ProjectPolicy> Policies { get; set; }

		public DbSet<User> Users { get; set; }


		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
		{
		
		}
	}
}
