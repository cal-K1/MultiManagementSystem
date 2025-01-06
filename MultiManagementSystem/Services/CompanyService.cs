using MultiManagementSystem.Data;
using MultiManagementSystem.Factories;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class CompanyService : ICompanyService
{
    private readonly IServiceProvider serviceProvider;
    private NavigationHelper? _navigationHelper;

    public Company? CurrentCompany { get; private set; }

    public CompanyService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    private NavigationHelper NavigationHelper
    {
        get
        {
            // Initialize navigationHelper lazily
            if (_navigationHelper == null)
            {
                _navigationHelper = serviceProvider.GetRequiredService<NavigationHelper>();
            }

            return _navigationHelper;
        }
    }

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
            NavigationHelper.Navigate("/company-info");
        }
        else if (notification.NotificationType == NotificationType.None)
        {
            NavigationHelper.Navigate("/login");
        }
    }
}
