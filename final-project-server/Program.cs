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
using System.Text.Json.Serialization;

namespace final_project_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddUserSecrets<Program>();

            // Configure JwtConfig options
            builder.Services.Configure<JwtPOCO>(builder.Configuration.GetSection("Jwt"));

            // Register AuthService as a singleton
            builder.Services.AddSingleton<JwtHelper>();


            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myCorsPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                    ValidIssuer = "PetitionBackEnd",
                    ValidAudience = "PetitionFrontEnd"
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("isAdmin", policy =>
                {
                    policy.RequireClaim("isAdmin", "True");
                });
            });

            builder.Services.AddScoped<IUserRepository, UserRepositoryEF>();
            builder.Services.AddScoped<IPolicyRepository, PolicyRepositoryEF>();
            builder.Services.AddScoped<IPoliciesService, PoliciesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var authService = scope.ServiceProvider.GetRequiredService<JwtHelper>();
            }

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
