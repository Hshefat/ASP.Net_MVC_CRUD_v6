using Microsoft.EntityFrameworkCore;

namespace MVC_CRUD_v6.Models
{
    public class HRDatabaseContext: DbContext 
    {
        public DbSet<Department> Departments { get; set; }   
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-ECVTMLP\SQLEXPRESS; Initial Catalog=EmployeesDB; integrated security=SSPI;");
        }
    }
}
