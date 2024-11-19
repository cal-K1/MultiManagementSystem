using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace Tests.ServiceTests.AuthorizationServiceTests
{
    public class IsLoginSuccessfulTests
    {
        private ManagementSystemDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
                //.UseInMemoryDatabase(databaseName: "TestDb") // Use an in-memory database
                .Options;

            var dbContext = new ManagementSystemDbContext(options);

            // Seed data
            dbContext.Workers.AddRange(new[]
            {
                new Worker { Id = "1", WorkerNumber = "123", Password = "password123" },
                new Worker { Id = "2", WorkerNumber = "456", Password = "password456" }
            });

            dbContext.SaveChanges();

            return dbContext;
        }

        [Theory]
        [InlineData("password123", "123", true)]  // Valid login
        [InlineData("wrongpassword", "123", false)] // Invalid password
        [InlineData("", "123", false)]           // Empty password
        [InlineData("password123", "", false)]   // Empty workerNumber
        [InlineData("password123", "789", false)] // Worker does not exist
        public void IsLoginSuccessful_ShouldValidateLoginCorrectly(string enteredPassword, string workerNumber, bool expected)
        {
            // Arrange
            using var dbContext = CreateDbContext();
            var service = new AuthorizationService(dbContext);

            // Act
            var result = service.IsLoginSuccessful(enteredPassword, workerNumber);

            // Assert
            result.Should().Be(expected);
        }
    }
}
