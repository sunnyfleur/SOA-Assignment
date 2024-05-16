using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SOA_Assignment.Models;
using System.IO;

public class YourDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }
    public Microsoft.EntityFrameworkCore.DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TestTable> TestTable { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customers> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
