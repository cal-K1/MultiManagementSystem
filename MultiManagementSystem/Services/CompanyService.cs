using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class CompanyService(ManagementSystemDbContext dbContext) : ICompanyService
{
    public Company CurrentCompany { get; private set; }

    public async Task CreateCompany(Company newCompany)
    {
        dbContext.Company.Add(newCompany);
        await dbContext.SaveChangesAsync();
    }

    public void SetCurrentCompany(string workerId)
    {
        string? companyId = dbContext.Workers.FirstOrDefault(w => w.Id == workerId)?.CompanyId;


    }

    public Company GetCurrentCompany(string companyId)
    {
        if (companyId != null)
        {
            CurrentCompany = dbContext.Company.FirstOrDefault(c => c.Id == companyId);

            if (CurrentCompany == null)
            {
                throw new InvalidOperationException("Company not found.");
            }
            return CurrentCompany;
        }
        else
        {
            throw new InvalidOperationException("Worker not found.");
        }
    }
}
