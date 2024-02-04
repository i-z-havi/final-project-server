using final_project_server.Services.Data;

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
            builder.Services.AddSingleton(serviceProvider =>
            {
                var config = serviceProvider.GetService<IConfiguration>();
                return MongoDbService.CreateMongoClient(config);
            });

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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
