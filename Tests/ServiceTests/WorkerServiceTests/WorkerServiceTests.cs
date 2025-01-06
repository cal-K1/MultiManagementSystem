using Microsoft.EntityFrameworkCore;
using Moq;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services;
using MultiManagementSystem.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using Xunit;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Tests
{
    public class WorkerServiceTests
    {
        private readonly WorkerService _workerService;
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly TestNavigationManager _testNavigationManager;
        private readonly ManagementSystemDbContext _dbContext;

        public class TestNavigationManager : NavigationManager
        {
            public string LastUri { get; private set; }

            public TestNavigationManager()
            {
                Initialize("http://localhost/", "http://localhost/");
            }

            protected override void NavigateToCore(string uri, bool forceLoad)
            {
                LastUri = uri;
            }
        }

        public WorkerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new ManagementSystemDbContext(options);

            _companyServiceMock = new Mock<ICompanyService>();
            _companyServiceMock.Setup(c => c.CurrentCompany).Returns(new Company { Id = "company123" });

            _testNavigationManager = new TestNavigationManager();

            _workerService = new WorkerService(_dbContext, _companyServiceMock.Object, new ConfigurationBuilder().Build(), _testNavigationManager);
        }

        public void ClearDatabase()
        {
            foreach (var entity in _dbContext.Set<Worker>())
            {
                _dbContext.Remove(entity);
            }
            _dbContext.SaveChanges();
        }

        [Fact]
        public void GetWorkerLeaveDaysRemaining_ShouldReturnCorrectLeaveDays_WhenWorkerExists()
        {
            // Arrange
            var worker = new Worker { Id = "worker123", LeaveDaysRemaining = 10 };
            _dbContext.Workers.Add(worker);
            _dbContext.SaveChanges();

            // Act
            var leaveDays = _workerService.GetWorkerLeaveDaysRemaining("worker123");

            // Assert
            Assert.Equal(10, leaveDays);

            ClearDatabase();
        }

        [Fact]
        public void GetWorkerLeaveDaysRemaining_ShouldReturnMinusOne_WhenWorkerDoesNotExist()
        {
            // Act
            var leaveDays = _workerService.GetWorkerLeaveDaysRemaining("worker123");

            // Assert
            Assert.Equal(-1, leaveDays);
        }

        [Fact]
        public async Task CreateNewWorkerInDb_ShouldAddWorkerToDb_WhenValidWorker()
        {
            // Arrange
            var worker = new Worker { Id = "worker123", WorkerNumber = "A123456", LeaveDaysRemaining = 10 };

            // Act
            await _workerService.CreateNewWorkerInDb(worker);

            var savedWorker = await _dbContext.Workers.FirstOrDefaultAsync(w => w.Id == worker.Id);
            Assert.NotNull(savedWorker);
            Assert.Equal(worker.Id, savedWorker?.Id);
            Assert.Equal(worker.WorkerNumber, savedWorker?.WorkerNumber);

            ClearDatabase();
        }

        [Fact]
        public void NavigateNotification_ShouldNavigateCorrectly_WhenNotificationIsLeaveRequest()
        {
            // Arrange
            var notification = new Notification { NotificationType = NotificationType.LeaveRequest };

            // Act
            _workerService.NavigateNotification(notification);

            // Assert
            Assert.Equal("/worker-details", _testNavigationManager.LastUri);
        }

        [Fact]
        public void NavigateNotification_ShouldNavigateCorrectly_WhenNotificationIsJobApplication()
        {
            // Arrange
            var notification = new Notification { NotificationType = NotificationType.JobApplication };

            // Act
            _workerService.NavigateNotification(notification);

            // Assert
            Assert.Equal("/worker-details", _testNavigationManager.LastUri);
        }

        [Fact]
        public void NavigateNotification_ShouldNavigateHome_WhenNotificationIsOtherType()
        {
            // Arrange
            var notification = new Notification { NotificationType = NotificationType.None };

            // Act
            _workerService.NavigateNotification(notification);

            // Assert
            Assert.Equal("/", _testNavigationManager.LastUri);
        }
    }
}
