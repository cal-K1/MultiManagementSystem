using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.People;

namespace MultiManagementSystem.Data
{
    public class MangementSystemDbContext : DbContext
    {
        public MangementSystemDbContext(DbContextOptions<MangementSystemDbContext> options) : base()
        {

        }

        public DbSet<JobApplication> JobApplication { get; set; }

        public DbSet<Management> Manager { get; set; }

        public DbSet<ContractWorker> ContractWorkers { get; set; }

        public DbSet<EmployeedWorker> EmployeedWorker { get; set; }

    }
}
