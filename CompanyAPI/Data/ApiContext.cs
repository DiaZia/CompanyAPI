using Microsoft.EntityFrameworkCore;
using CompanyAPI.Models;


namespace CompanyAPI.Data
{
    public class ApiContext : DbContext
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {

        }
    }
}
