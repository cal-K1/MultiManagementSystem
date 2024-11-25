using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;
using static System.Formats.Asn1.AsnWriter;

namespace MultiManagementSystem.Services;

public class CompanyService(IServiceProvider _serviceProvider) : ICompanyService
{
    private readonly IServiceProvider _serviceProvider;
    public Company? CurrentCompany { get; private set; }

    public async Task CreateCompany(Company newCompany)
    {
        var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        dbContext.Company.Add(newCompany);
        await dbContext.SaveChangesAsync();
    }

    public void SetCurrentCompany(string workerId)
    {
        var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        string? companyId = dbContext.Workers.FirstOrDefault(w => w.Id == workerId)?.CompanyId;

        if (companyId == null)
        {
            throw new InvalidOperationException("Worker not found.");
        }

        CurrentCompany = GetCurrentCompany(companyId);
    }

    public Company GetCurrentCompany(string companyId)
    {
        if (companyId == null)
        {
            throw new InvalidOperationException("Worker not found.");
        }
        else
        {
            var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

            CurrentCompany = dbContext.Company.FirstOrDefault(c => c.Id == companyId);

            if (CurrentCompany == null)
            {
                throw new InvalidOperationException("Company not found.");
            }
        }

        return CurrentCompany;
    }
}
