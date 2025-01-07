using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Services;
using MultiManagementSystem.Models;
using MultiManagementSystem.Data;
using System;
using System.Linq;
using Xunit;

namespace MultiManagementSystem.Tests
{
    public class ApplicationServiceTests : IDisposable
    {
        private readonly ManagementSystemDbContext dbContext;
        private readonly ApplicationService applicationService;

        public ApplicationServiceTests()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            dbContext = new ManagementSystemDbContext(options);
            applicationService = new ApplicationService(dbContext);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            dbContext.JobApplications.Add(new JobApplication
            {
                Id = "1",
                ApplicantId = "A123",
                ApplicantName = "John Doe",
                ApplicantPhoneNumber = "123-456-7890",
                ApplicationText = "I am a good fit for the job.",
                ApplicationState = ApplicationState.Pending
            });
            dbContext.SaveChanges();
        }

        [Fact]
        public void GetApplication_ValidId_ReturnsApplication()
        {
            // Arrange
            string applicationId = "1";

            // Act
            var application = applicationService.GetApplication(applicationId);

            // Assert
            Assert.NotNull(application);
            Assert.Equal(applicationId, application.Id);
            Assert.Equal("John Doe", application.ApplicantName);
            Assert.Equal(ApplicationState.Pending, application.ApplicationState);
        }

        [Fact]
        public void GetApplication_InvalidId_ThrowsException()
        {
            // Arrange
            string applicationId = "999";

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => applicationService.GetApplication(applicationId));
            Assert.Equal($"Job application with ID {applicationId} not found.", exception.Message);
        }

        public void Dispose()
        {
            // Ensure the database is cleared after each test
            dbContext.JobApplications.RemoveRange(dbContext.JobApplications);
            dbContext.SaveChanges();
            dbContext.Dispose();
        }
    }
}
