using MultiManagementSystem.Data;

namespace MultiManagementSystem.Services;

public class CompanyService(ManagementSystemDbContext dbContext)
{
    async Task CreateCompany(Company newCompany)
    {
        dbContext.Company.Add(newCompany);
        await dbContext.SaveChangesAsync();
    }
}
