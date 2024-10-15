using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.People;
using System.Reflection;

namespace MultiManagementSystem.Data
{
    public class ManagementSystemDbContext : DbContext
    {
        public ManagementSystemDbContext(DbContextOptions<ManagementSystemDbContext> options)
            : base(options) // This line passes the options to the base DbContext
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Management>().ToTable("Manager").HasKey(a => a.Id);
            modelBuilder.Entity<JobApplication>().ToTable("JobApplication").HasKey(j => j.Id);
            modelBuilder.Entity<ContractWorker>().ToTable("ContractWorker").HasKey(c => c.Id);
            modelBuilder.Entity<EmployedWorker>().ToTable("EmployedWorker").HasKey(e => e.Id);

            base.OnModelCreating(modelBuilder);
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=MultiManagementSystem; Integrated Security=True; trustServerCertificate=true");
            }
        }

        public DbSet<Management> Manager { get; set; }
        public DbSet<JobApplication> JobApplication { get; set; }
        public DbSet<ContractWorker> ContractWorkers { get; set; }
        public DbSet<EmployedWorker> EmployeedWorker { get; set; }
    }
}
