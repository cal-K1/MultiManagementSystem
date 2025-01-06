using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Factories;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class CompanyService(IServiceProvider serviceProvider) : ICompanyService
{    public Company? CurrentCompany { get; private set; }

    NavigationHelper navigationHelper;

    public void SetCurrentCompanyByCompanyId(Company company)
    {
        CurrentCompany = company;
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

    public void SetCurrentCompanyByCompanyId(string companyId)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

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

     public Company GetCurrentCompany(string companyId)
     {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

         return dbContext.Company.FirstOrDefault(c => c.Id == companyId)
            ?? throw new InvalidOperationException("Company not found for the given ID.");
     }

    public void NavigateNotificationClick(Notification notification)
    {
        if (notification == null)
        {
            throw new ArgumentNullException(nameof(notification));
        }

        if (notification.NotificationType == NotificationType.JobApplication)
        {
            navigationHelper.Navigate("/company-info");
        }
        else if (notification.NotificationType == NotificationType.None)
        {
            navigationHelper.Navigate("/login");
        }
    }
}
