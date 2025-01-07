using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Services.Abstraction;
using MultiManagementSystem.Services;
using MultiManagementSystem.Models.People;
using Microsoft.Extensions.Configuration;
using Xunit;
using Microsoft.AspNetCore.Components;

public class DatabaseServiceTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ManagementSystemDbContext _dbContext;
    private readonly ICompanyService _companyService;
    private readonly IDatabaseService _databaseService;

    public DatabaseServiceTests()
    {
        var services = new ServiceCollection();

        // Register In-Memory Database
        services.AddDbContext<ManagementSystemDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        // Register mock NavigationManager
        var mockNavigationManager = new Mock<NavigationManager>();
        services.AddSingleton(mockNavigationManager.Object);

        // Register other services
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDatabaseService, DatabaseService>();

        // Mock or stub WorkerService
        var mockWorkerService = new Mock<IWorkerService>();
        services.AddSingleton(mockWorkerService.Object);

        services.AddScoped<IAuthorizationService, AuthorizationService>();

        var mockConfig = new Mock<IConfiguration>();
        services.AddSingleton(mockConfig.Object);

        // Build service provider
        _serviceProvider = services.BuildServiceProvider();

        // Initialize required services
        _dbContext = _serviceProvider.GetRequiredService<ManagementSystemDbContext>();
        _companyService = _serviceProvider.GetRequiredService<ICompanyService>();
        _databaseService = _serviceProvider.GetRequiredService<IDatabaseService>();

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }


    [Fact]
    public async Task IsLoginSuccessful_ReturnsFalseForInvalidCredentials()
    {
        // Arrange
        var worker = new Worker { Id = Guid.NewGuid().ToString(), WorkerNumber = "worker001", Password = "password123" };
        _dbContext.Workers.Add(worker);
        await _dbContext.SaveChangesAsync();
        var enteredPassword = "wrongPassword";
        var workerNumber = "worker001";

        // Act
        var result = await _databaseService.SetUserLoggedIn(enteredPassword, workerNumber);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetWorkerById_ReturnsWorkerWhenExists()
    {
        // Arrange
        var worker = new Worker { Id = "worker001", WorkerNumber = "worker001" };
        _dbContext.Workers.Add(worker);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _databaseService.GetWorkerById(worker.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(worker.Id, result.Id);
    }

    [Fact]
    public async Task GetWorkerById_ReturnsNullWhenNotExists()
    {
        // Act
        var result = await _databaseService.GetWorkerById("nonExistingId");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAdmin_AdminCreatedSuccessfully()
    {
        // Arrange
        var adminUsername = "adminUser";
        var adminPassword = "adminPass";

        var mockCompanyService = new Mock<ICompanyService>();
        var company = new Company { Id = "company001", CompanyName = "Test Company" };
        mockCompanyService.Setup(c => c.CurrentCompany).Returns(company);

        var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        var dbContext = new ManagementSystemDbContext(options);

        var serviceProvider = new ServiceCollection()
            .AddSingleton(mockCompanyService.Object)
            .AddDbContext<ManagementSystemDbContext>(opt => opt.UseInMemoryDatabase("TestDb"))
            .BuildServiceProvider();

        var databaseService = new DatabaseService(serviceProvider, dbContext, mockCompanyService.Object);

        // Act
        await databaseService.CreateAdmin(adminUsername, adminPassword);

        // Assert
        var admin = await dbContext.Administrator
            .FirstOrDefaultAsync(a => a.Username == adminUsername);

        Assert.NotNull(admin);
        Assert.Equal(adminUsername, admin.Username);
    }


    [Fact]
    public async Task GetAllJobRolesByCompanyId_ReturnsJobRoles()
    {
        // Arrange
        var companyId = "company001";
        _dbContext.JobRole.Add(new JobRole { CompanyId = companyId, JobTitle = "Developer" });
        await _dbContext.SaveChangesAsync();

        // Act
        var jobRoles = await _databaseService.GetAllJobRolesByCompanyId(companyId);

        // Assert
        Assert.NotEmpty(jobRoles);
        Assert.Equal("Developer", jobRoles.First().JobTitle);
    }

    [Fact]
    public async Task CreateCompany_CompanyCreatedSuccessfully()
    {
        // Arrange
        var company = new Company { CompanyName = "Test Company", Id = "company001" };

        // Act
        await _databaseService.CreateCompany(company);

        // Assert
        var createdCompany = await _dbContext.Company
            .FirstOrDefaultAsync(c => c.Id == company.Id);

        Assert.NotNull(createdCompany);
        Assert.Equal("Test Company", createdCompany.CompanyName);
    }

    [Fact]
    public async Task GetAllPendingJobApplications_ReturnsPendingApplications()
    {
        // Arrange
        var jobApplication = new JobApplication
        {
            Id = Guid.NewGuid().ToString(),
            ApplicantId = "Applicant123", // Add necessary fields
            ApplicantName = "John Doe",
            ApplicantPhoneNumber = "123-456-7890",
            ApplicationText = "I am a good fit.",
            ApplicationState = ApplicationState.Pending,
            JobRole = new JobRole { Id = "FullTime", JobTitle = "Full-Time" }
        };

        _dbContext.JobApplications.Add(jobApplication);
        await _dbContext.SaveChangesAsync();

        // Act
        var pendingApplications = await _databaseService.GetAllPendingJobApplications();

        // Assert
        Assert.Contains(pendingApplications, app => app.ApplicationState == ApplicationState.Pending);
    }


    [Fact]
    public async Task GetAllPendingLeaveRequestsByCompanyId_ReturnsPendingLeaveRequests()
    {
        // Arrange
        var companyId = "company001";
        var leaveRequest = new LeaveRequest
        {
            Id = Guid.NewGuid().ToString(),
            Worker = new Worker { Id = Guid.NewGuid().ToString(), CompanyId = companyId },
            State = LeaveRequestState.Pending
        };
        _dbContext.LeaveRequests.Add(leaveRequest);
        await _dbContext.SaveChangesAsync();

        // Act
        var pendingLeaveRequests = await _databaseService.GetAllPendingLeaveRequestsByCompanyId(companyId);

        // Assert
        Assert.Contains(pendingLeaveRequests, lr => lr.State == LeaveRequestState.Pending);
    }

    [Fact]
    public async Task AcceptApplication_ApplicationAccepted()
    {
        // Arrange
        var jobApplication = new JobApplication
        {
            Id = Guid.NewGuid().ToString(),
            ApplicantId = "Applicant123", // Add necessary fields
            ApplicantName = "John Doe",
            ApplicantPhoneNumber = "123-456-7890",
            ApplicationText = "I am a good fit.",
            ApplicationState = ApplicationState.Pending,
            JobRole = new JobRole { Id = "FullTime", JobTitle = "Full-Time" }
        };

        _dbContext.JobApplications.Add(jobApplication);
        await _dbContext.SaveChangesAsync();

        // Act
        await _databaseService.AcceptApplication(jobApplication);

        // Assert
        var updatedApplication = await _dbContext.JobApplications
            .FirstOrDefaultAsync(app => app.Id == jobApplication.Id);
        Assert.Equal(ApplicationState.Accepted, updatedApplication.ApplicationState);
    }

    [Fact]
    public async Task DeclineApplication_ApplicationDeclined()
    {
        // Arrange
        var jobApplication = new JobApplication
        {
            Id = Guid.NewGuid().ToString(),
            ApplicantId = "Applicant123", // Add necessary fields
            ApplicantName = "John Doe",
            ApplicantPhoneNumber = "123-456-7890",
            ApplicationText = "I am a good fit.",
            ApplicationState = ApplicationState.Pending,
            JobRole = new JobRole { Id = "Contract", JobTitle = "Contract" }
        };

        _dbContext.JobApplications.Add(jobApplication);
        await _dbContext.SaveChangesAsync();

        // Act
        await _databaseService.DeclineApplication(jobApplication);

        // Assert
        var updatedApplication = await _dbContext.JobApplications
            .FirstOrDefaultAsync(app => app.Id == jobApplication.Id);
        Assert.Equal(ApplicationState.Declined, updatedApplication.ApplicationState);
    }

    [Fact]
    public async Task ApplyJob_JobApplicationCreated()
    {
        // Arrange
        var applicantId = "worker001";
        var name = "John Doe";
        var phoneNumber = "1234567890";
        var applicationText = "I am interested in this job.";

        // Act
        await _databaseService.ApplyJob(applicantId, name, phoneNumber, applicationText);

        // Assert
        var application = await _dbContext.JobApplications
            .FirstOrDefaultAsync(app => app.ApplicantId == applicantId);

        Assert.NotNull(application);
        Assert.Equal(applicantId, application.ApplicantId);
        Assert.Equal(ApplicationState.Pending, application.ApplicationState);
    }

    [Fact]
    public async Task AddNewLeaveRequest_LeaveRequestAdded()
    {
        // Arrange
        var worker = new Worker { Id = "worker001", CompanyId = "company001" };
        var leaveRequest = new LeaveRequest { Worker = worker, State = LeaveRequestState.Pending };

        // Act
        await _databaseService.AddNewLeaveRequest(worker, leaveRequest);

        // Assert
        var addedLeaveRequest = await _dbContext.LeaveRequests
            .FirstOrDefaultAsync(lr => lr.Worker == worker);

        Assert.NotNull(addedLeaveRequest);
        Assert.Equal(LeaveRequestState.Pending, addedLeaveRequest.State);
    }

    [Fact]
    public async Task GetWorkerByWorkerNumber_ReturnsWorker()
    {
        // Arrange
        var worker = new Worker { WorkerNumber = "worker001", Id = "worker001" };
        _dbContext.Workers.Add(worker);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _databaseService.GetWorkerByWorkerNumber(worker.WorkerNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(worker.WorkerNumber, result.WorkerNumber);
    }

    [Fact]
    public async Task GetWorkersByCompanyId_ReturnsWorkers()
    {
        // Arrange
        var companyId = "company001";
        var worker = new Worker { CompanyId = companyId, Id = "worker001", WorkerNumber = "worker001" };
        _dbContext.Workers.Add(worker);
        await _dbContext.SaveChangesAsync();

        // Act
        var workers = await _databaseService.GetWorkersByCompanyId(companyId);

        // Assert
        Assert.Contains(workers, w => w.CompanyId == companyId);
    }

    [Fact]
    public async Task SaveJobRoleToWorker_WorkerJobRoleSaved()
    {
        // Arrange
        var worker = new Worker { Id = "worker001", WorkerNumber = "worker001" };
        var jobRole = new JobRole { Id = "role001", JobTitle = "Developer" };
        _dbContext.Workers.Add(worker);
        _dbContext.JobRole.Add(jobRole);
        await _dbContext.SaveChangesAsync();

        // Act
        await _databaseService.SaveJobRoleToWorker(worker, jobRole.Id);

        // Assert
        var updatedWorker = await _dbContext.Workers
            .FirstOrDefaultAsync(w => w.Id == worker.Id);
        Assert.Equal(jobRole.Id, updatedWorker.JobRoleId);
    }

    [Fact]
    public async Task RemoveWorker_WorkerRemoved()
    {
        // Arrange
        var worker = new Worker { Id = "worker001", CompanyId = "company001" };
        _dbContext.Workers.Add(worker);
        await _dbContext.SaveChangesAsync();

        // Act
        _databaseService.RemoveWorker(worker);

        // Assert
        var removedWorker = await _dbContext.Workers
            .FirstOrDefaultAsync(w => w.Id == worker.Id);
        Assert.Null(removedWorker);
    }

    [Fact]
    public async Task CreateNewWorkerInDb_WorkerCreated()
    {
        // Arrange
        var worker = new Worker { Id = "worker001", WorkerNumber = "worker001", JobRoleId = "role001" };

        var mockCompanyService = new Mock<ICompanyService>();
        var company = new Company { Id = "company001", CompanyName = "Test Company" };
        mockCompanyService.Setup(c => c.CurrentCompany).Returns(company);

        var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        var dbContext = new ManagementSystemDbContext(options);

        var serviceProvider = new ServiceCollection()
            .AddSingleton(mockCompanyService.Object)
            .AddDbContext<ManagementSystemDbContext>(opt => opt.UseInMemoryDatabase("TestDb"))
            .BuildServiceProvider();

        var databaseService = new DatabaseService(serviceProvider, dbContext, mockCompanyService.Object);

        // Act
        await databaseService.CreateNewWorkerInDb(worker);

        // Assert
        var createdWorker = await dbContext.Workers
            .FirstOrDefaultAsync(w => w.Id == worker.Id);
        Assert.NotNull(createdWorker);
        Assert.Equal("worker001", createdWorker.WorkerNumber);
    }

    [Theory]
    [InlineData("password123", "worker001", true)]    // Valid login
    [InlineData("wrongpassword", "worker001", false)] // Invalid password
    [InlineData("", "worker001", false)]              // Empty password
    [InlineData("password123", "", false)]            // Empty workerNumber
    [InlineData("password123", "nonexistent", false)] // Worker does not exist
    public void WorkerValidLogin_ShouldValidateLoginCorrectly(string enteredPassword, string workerNumber, bool expected)
    {
        // Arrange
        using var dbContext = CreateDbContext();  
        var worker = new Worker
        {
            Id = "worker001",
            WorkerNumber = "worker001",
            Password = "password123"
        };
        dbContext.Workers.Add(worker);
        dbContext.SaveChanges();  

        var service = new DatabaseService(
            _serviceProvider,
            dbContext,       
            _companyService  
        );

        // Act
        var result = service.WorkerValidLogin(enteredPassword, workerNumber);

        // Assert
        Assert.Equal(expected, result);
    }
    
    private ManagementSystemDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        return new ManagementSystemDbContext(options);
    }
}
