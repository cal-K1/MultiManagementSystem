using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.People;
using System.Reflection;

namespace MultiManagementSystem.Data
{
    public class ManagementSystemDbContext : DbContext
    {
        public ManagementSystemDbContext(DbContextOptions<ManagementSystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations from the assembly (if there are any configuration classes)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Ignore the Worker base class to prevent creating a table for it
            modelBuilder.Ignore<Worker>();

            // Configure each derived entity explicitly
            modelBuilder.Entity<Management>()
                .ToTable("Managers")
                .HasKey(m => m.Id);

            modelBuilder.Entity<JobApplication>()
                .ToTable("JobApplications")
                .HasKey(j => j.Id);

            // Configure ContractWorker
            modelBuilder.Entity<ContractWorker>()
                .ToTable("ContractWorkers")
                .HasKey(c => c.Id);

            // Configure EmployedWorker
            modelBuilder.Entity<EmployedWorker>()
                .ToTable("EmployedWorkers")
                .HasKey(e => e.Id);

            modelBuilder.Entity<LeaveRequest>()
                .ToTable("LeaveRequests")
                .HasKey(l => l.Id);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=MultiManagementSystem; Integrated Security=True; TrustServerCertificate=True");
            }
        }

        // DbSet properties for each entity
        public DbSet<Management> Managers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<ContractWorker> ContractWorkers { get; set; }
        public DbSet<EmployedWorker> EmployedWorkers { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
