using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.People;

namespace MultiManagementSystem.Data
{
    public class ManagementDbContext : DbContext
    {
        public ManagementDbContext(DbContextOptions<ManagementDbContext> options) : base()
        {

        }

    }
}
