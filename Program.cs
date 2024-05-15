using SOA_Assignment.Models;
using SOA_Assignment.Services;
using SOA_Assignment;
using Microsoft.Extensions.Options;
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

            //builder.Services.Configure<List<BackendService>>(
            //    builder.Configuration.GetSection("BackendServices"));
            //builder.Services.AddSingleton<ILoadBalancerService, LoadBalancerService>();
            builder.Services.AddDbContext<YourDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // C?u hình xác th?c JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero // Th?i gian chênh l?ch cho phép gi?a client và server
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication(); // Kích ho?t xác th?c
            app.UseAuthorization();  // Kích ho?t ?y quy?n

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}