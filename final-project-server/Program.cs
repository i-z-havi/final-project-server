using final_project_server.Data;
using final_project_server.Middleware;
using final_project_server.Services.Data;
using final_project_server.Services.Policies;
using final_project_server.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace final_project_server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("LaptopConnection"))
			);

			builder.Services.AddScoped<IPoliciesService, PoliciesService>();
			builder.Services.AddScoped<IUsersService, UsersService>();
			
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("myCorsPolicy", policy =>
				{
					policy.WithOrigins("*")
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("myCorsPolicy");
			app.UseHttpsRedirection();
			app.UseMiddleware<LoggerMiddleware>();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
