using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Services.Abstraction;
using Xunit;

namespace Tests.ServiceTests.AuthorizationServiceTests
{
    public class AuthorizationServiceTests
    {
        private ManagementSystemDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ManagementSystemDbContext>()
                .Options;

            return new ManagementSystemDbContext(options);
        }

        [Theory]
        [InlineData("John", true)]                                                            // Valid username
        [InlineData("", false)]                                                               // Empty string
        [InlineData(null, false)]                                                             // Null value
        [InlineData("A", false)]                                                              // Too short
        [InlineData("The length of this string is sixty characters, it is allowed", true)]    // Max valid length
        [InlineData("This string has a length of sixty-one characters, not allowed", false)]  // Too long
        public void IsUserNameValid_ShouldValidateUserNameCorrectly(string userName, bool expected)
        {
            // Arrange
            using var dbContext = CreateDbContext(); // Initialize DbContext
            var service = new AuthorizationService(dbContext);

            // Act
            var result = service.IsUserNameValid(userName);

            // Assert
            result.Should().Be(expected);
        }
    }
}
