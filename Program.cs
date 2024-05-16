using SOA_Assignment.Models;
using SOA_Assignment.Services;
using SOA_Assignment;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SOA_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<YourDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.Configure<List<BackendService>>(
                builder.Configuration.GetSection("BackendServices"));
            builder.Services.AddSingleton<ILoadBalancerService, LoadBalancerService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}