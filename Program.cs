using SOA_Assignment.Models;
using SOA_Assignment.Services;
using SOA_Assignment;
using Microsoft.Extensions.Options;

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

            builder.Services.Configure<List<BackendService>>(
                builder.Configuration.GetSection("BackendServices"));
            builder.Services.AddSingleton<ILoadBalancerService, LoadBalancerService>();

            // Thêm dịch vụ CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:5174")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Áp dụng chính sách CORS trước UseAuthorization
            app.UseCors("AllowSpecificOrigin");

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}