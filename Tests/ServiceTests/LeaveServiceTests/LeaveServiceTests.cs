using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Services;
using MultiManagementSystem.Models;
using MultiManagementSystem.Data;
using Moq;
using System;
using System.Linq;
using Xunit;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;
using NSubstitute;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata;

namespace MultiManagementSystem.Tests
{
    public class LeaveServiceTests : IDisposable
    {
        private readonly ManagementSystemDbContext dbContext;
        private readonly LeaveService leaveService;
        private readonly Mock<IWorkerService> mockWorkerService;

        public LeaveServiceTests()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
                .UseInMemoryDatabase("TestDatabase_" + Guid.NewGuid()) // Use a unique name for each test run
                .Options;

            dbContext = new ManagementSystemDbContext(options);
            mockWorkerService = new Mock<IWorkerService>();
            leaveService = new LeaveService(dbContext)
            {
                workerService = mockWorkerService.Object // Inject mocked IWorkerService
            };

            // Seed the database with sample worker data
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            var worker = new Worker
            {
                Id = "1",
                WorkerNumber = "W001",
                Manager = false,
                Password = "password",
                JobRoleId = "J001",
                CompanyId = "C001",
                LeaveDaysRemaining = 10
            };

            dbContext.Workers.Add(worker);
            dbContext.SaveChanges();
        }

        [Fact]
        public void AcceptLeave_ValidLeave_UpdatesLeaveDaysRemaining()
        {
            // Arrange
            string workerId = "1";
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(5); // 5 days of leave
            var worker = dbContext.Workers.FirstOrDefault(w => w.Id == workerId);

            // Mock IWorkerService to return 10 remaining leave days
            mockWorkerService.Setup(ws => ws.GetWorkerLeaveDaysRemaining(workerId)).Returns(10);

            // Act
            leaveService.AcceptLeave(startDate, endDate, worker);

            // Assert
            Assert.Equal(5, worker.LeaveDaysRemaining);
        }

        [Fact]
        public void AcceptLeave_InsufficientLeave_DoesNotUpdateLeaveDaysRemaining()
        {
            // Arrange
            string workerId = "1";
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(15); // 15 days of leave
            var worker = dbContext.Workers.FirstOrDefault(w => w.Id == workerId);

            // Mock IWorkerService to return 10 remaining leave days
            mockWorkerService.Setup(ws => ws.GetWorkerLeaveDaysRemaining(workerId)).Returns(10);

            // Act
            leaveService.AcceptLeave(startDate, endDate, worker);

            // Assert
            Assert.Equal(10, worker.LeaveDaysRemaining);
        }

        [Fact]
        public void AcceptLeave_WorkerNotFound_ThrowsException()
        {
            // Arrange
            string workerId = "999"; // Non-existing worker
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(5); // 5 days of leave
            var worker = dbContext.Workers.FirstOrDefault(w => w.Id == workerId);

            mockWorkerService.Setup(ws => ws.GetWorkerLeaveDaysRemaining(workerId)).Returns(10);

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => leaveService.AcceptLeave(startDate, endDate, worker));
            Assert.Equal("Value cannot be null. (Parameter 'worker')", exception.Message);
        }

        public void Dispose()
        {
            // Ensure the database is cleared after each test to avoid "item with the same key" errors
            dbContext.Database.EnsureDeleted(); // Ensures the in-memory database is deleted
            dbContext.Dispose();
        }
    }
}
