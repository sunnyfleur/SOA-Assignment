using SOA_Assignment.Models;
using SOA_Assignment.Services;
using Microsoft.Extensions.Options;

namespace SOA_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
<<<<<<< Updated upstream

            builder.Services.Configure<List<BackendService>>(
                builder.Configuration.GetSection("BackendServices"));
            builder.Services.AddSingleton<ILoadBalancerService, LoadBalancerService>();
=======
            builder.Services.AddMemoryCache();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            builder.Services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDbContext<YourDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5173") // Use the correct port for your frontend
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
>>>>>>> Stashed changes

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
<<<<<<< Updated upstream
=======

            // Use CORS policy before other middleware
            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();
            app.UseMiddleware<RateLimitingMiddleware>();

>>>>>>> Stashed changes
            app.UseAuthorization();

            app.MapControllers();
<<<<<<< Updated upstream
            app.Run();
        }
    }
}
=======

            app.Run();
        }
    }
}
>>>>>>> Stashed changes
