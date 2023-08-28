using Microsoft.EntityFrameworkCore;
using CompanyAPI.Models;


namespace CompanyAPI.Data
{
    public class ApiContext : DbContext
    {

        public DbSet<Company> Company { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            :base(options)
        {
        }
    }
}
