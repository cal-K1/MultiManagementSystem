using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Services;
using MultiManagementSystem.Models.People;
using Xunit;
using Moq;
using MultiManagementSystem.Factories;
using Microsoft.AspNetCore.Components;

public class CompanyServiceTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly ManagementSystemDbContext _dbContext;
    private readonly Mock<NavigationHelper> _navigationHelperMock;

    public CompanyServiceTests()
    {
        var serviceCollection = new ServiceCollection();

        // Configure InMemoryDatabase
        serviceCollection.AddDbContext<ManagementSystemDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _dbContext = _serviceProvider.GetRequiredService<ManagementSystemDbContext>();

        // Ensure fresh DB state
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        // Clean up the in-memory DB after each test
        _dbContext.Database.EnsureDeleted();
    }


    [Fact]
    public void SetCurrentCompanyByCompanyId_Should_Set_CurrentCompany()
    {
        // Arrange
        var company = new Company { Id = "3", CompanyName = "Test Company3" };
        _dbContext.Company.Add(company);
        _dbContext.SaveChanges();

        var companyService = new CompanyService(_serviceProvider);

        // Act
        companyService.SetCurrentCompanyByCompanyId(company);

        // Assert
        Assert.Equal(company, companyService.CurrentCompany);
    }

    [Fact]
    public void SetCurrentCompany_Should_Set_CurrentCompany_By_WorkerId()
    {
        // Arrange
        var company = new Company { Id = "4", CompanyName = "Test Company4" };
        var worker = new Worker { Id = "5", CompanyId = "4" };
        _dbContext.Company.Add(company);
        _dbContext.Workers.Add(worker);
        _dbContext.SaveChanges();

        var companyService = new CompanyService(_serviceProvider);

        // Act
        companyService.SetCurrentCompany(worker.Id);

        // Assert
        Assert.Equivalent(company, companyService.CurrentCompany);
    }

    [Fact]
    public void SetCurrentCompany_Should_Throw_If_Worker_Not_Found()
    {
        // Arrange
        var companyService = new CompanyService(_serviceProvider);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => companyService.SetCurrentCompany("invalid_worker_id"));
    }

    [Fact]
    public void NavigateNotificationClick_Should_Throw_If_Notification_Is_Null()
    {
        // Arrange
        var companyService = new CompanyService(_serviceProvider);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => companyService.NavigateNotificationClick(null));
    }
}
