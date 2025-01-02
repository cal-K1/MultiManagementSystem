using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class CompanyService(IServiceProvider serviceProvider) : ICompanyService
{    public Company? CurrentCompany { get; private set; }

    public async Task CreateCompany(Company newCompany)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        dbContext.Company.Add(newCompany);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error saving new company.", ex);
        }

        CurrentCompany = newCompany;
    }


    public void SetCurrentCompany(string workerId)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        string? companyId = dbContext.Workers.FirstOrDefault(w => w.Id == workerId)?.CompanyId;

        if (companyId == null)
        {
            throw new InvalidOperationException("Worker not found or not assigned to a company.");
        }

        CurrentCompany = GetCurrentCompany(companyId);
    }

    public void SetCurrentCompanyAsAdmin(Admin admin)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        string? companyId = dbContext.Company.FirstOrDefault(w => w.Admin.Id == admin.Id)?.Id;

        if (companyId == null)
        {
            throw new InvalidOperationException("Worker not found or not assigned to a company.");
        }

        CurrentCompany = GetCurrentCompany(companyId);
    }

    public async Task CreateAdmin(string adminUsername, string adminPassword)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        if (CurrentCompany == null)
        {
            throw new InvalidOperationException("No company selected.");
        }

        Admin newAdmin = new Admin
        {
            Username = adminUsername,
            Password = adminPassword,
            Id = Guid.NewGuid().ToString()
        };

        CurrentCompany.Admin = newAdmin;

        dbContext.Administrator.Add(newAdmin);
        await dbContext.SaveChangesAsync();
    }

     public Company GetCurrentCompany(string companyId)
     {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

         return dbContext.Company.FirstOrDefault(c => c.Id == companyId)
            ?? throw new InvalidOperationException("Company not found for the given ID.");
     }

    public async Task<List<JobRole>> GetAllJobRolesByCompanyId(string companyId)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        return await dbContext.JobRole
            .Where(j => j.CompanyId == companyId)
            .ToListAsync();
    }
}
