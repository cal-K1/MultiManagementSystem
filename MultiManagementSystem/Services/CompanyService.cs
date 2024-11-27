﻿using MultiManagementSystem.Data;
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

        SetCurrentCompany(newCompany.Id);
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

    public Company GetCurrentCompany(string companyId)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        return dbContext.Company.FirstOrDefault(c => c.Id == companyId)
               ?? throw new InvalidOperationException("Company not found for the given ID.");
    }
}
