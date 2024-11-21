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

            // Configure derived entities explicitly
            modelBuilder.Entity<Management>()
                .ToTable("Managers")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Worker>()
                .ToTable("Workers")
                .HasKey(w => w.Id);

            // Configure the Worker entity and the embedded JobRole
            modelBuilder.Entity<Worker>(entity =>
            {
                entity.Property(w => w.Id).IsRequired();
                entity.Property(w => w.Name).IsRequired();
                entity.Property(w => w.WorkerNumber).IsRequired();
                entity.Property(w => w.Password).IsRequired();
                entity.Property(w => w.Manager).IsRequired();
                entity.Property(w => w.Country).IsRequired();

                // Configure the JobRole as an owned entity within Worker
                entity.OwnsOne(w => w.JobRole, jobRole =>
                {
                    jobRole.Property(j => j.JobTitle).IsRequired();
                    jobRole.Property(j => j.Salary).IsRequired();
                    jobRole.Property(j => j.Description).IsRequired();
                });
            });

            modelBuilder.Entity<UserId>()
                .ToTable("UserId")
                .HasKey(u => u.Id);

            modelBuilder.Entity<JobApplication>()
                .ToTable("JobApplications")
                .HasKey(j => j.Id);

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
        public DbSet<Worker> Workers { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<UserId> UserId { get; set; }

    }
}
