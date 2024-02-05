using ASPNETMVCCURUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCURUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
