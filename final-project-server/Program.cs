using final_project_server.Authentication;
using final_project_server.Middleware;
using final_project_server.Services.Data;
using final_project_server.Services.Data.Repositories.Interfaces;
using final_project_server.Services.Data.Repositories.Policies;
using final_project_server.Services.Data.Repositories.Users;
using final_project_server.Services.Policies;
using final_project_server.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

			builder.Services.AddSingleton(serviceProvider =>
			{
				var configuration = serviceProvider.GetService<IConfiguration>();
				return MongoDbService.CreateMongoClient(configuration);
			});

			//change this later!
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("myCorsPolicy", policy =>
				{
					policy.WithOrigins("http://localhost:3000")
					.AllowAnyMethod()
					.AllowAnyHeader();
				});
			});

            //THIS IS WHAT HTTPCONTEXT WORKS OFF OF!!
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer= true,
					ValidateAudience=true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtHelper.secretKey))
				};
			});

			builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();
			builder.Services.AddScoped<IPolicyRepository, PolicyRepositoryEF>();
			builder.Services.AddScoped<IPoliciesService, PoliciesService>();
			builder.Services.AddScoped<IUsersService, UsersService>();


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
