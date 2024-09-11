using Latihan.Models;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options): base(options) { }
        public DbSet<University> Universities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
    }
}
