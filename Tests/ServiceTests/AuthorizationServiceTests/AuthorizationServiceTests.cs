using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;
using Xunit;

namespace MultiManagementSystem.Tests.Services
{
    public class AuthorizationServiceTests
    {
        private readonly AuthorizationService _authorizationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICompanyService _companyService;
        private readonly MockManagementSystemDbContext _mockDbContext;

        public AuthorizationServiceTests()
        {
            // Manually create mock instances of the dependencies
            _mockDbContext = new MockManagementSystemDbContext();
            _companyService = new MockCompanyService();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<ICompanyService>(_companyService);
            serviceCollection.AddSingleton<IServiceProvider>(sp => serviceCollection.BuildServiceProvider());

            // Set up a service provider to return the mock db context
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Create instance of AuthorizationService with mocked dependencies
            _authorizationService = new AuthorizationService(_serviceProvider, _companyService);
        }

        [Fact]
        public void IsUserNameValid_ValidUserName_ReturnsTrue()
        {
            // Arrange
            string userName = "ValidUser";

            // Act
            bool result = _authorizationService.IsUserNameValid(userName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsUserNameValid_InvalidUserName_ReturnsFalse()
        {
            // Arrange
            string userName = "A";
            string userName2 = "ThisUserNameIsWayTooLongForValidationThisUserNameIsWayTooLongForValidation";

            // Act
            bool result1 = _authorizationService.IsUserNameValid(userName);
            bool result2 = _authorizationService.IsUserNameValid(userName2);

            // Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void IsPasswordValid_ValidPassword_ReturnsTrue()
        {
            // Arrange
            string password = "Password123";

            // Act
            bool result = _authorizationService.IsPasswordValid(password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPasswordValid_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            string password = "password"; // No uppercase or digit
            string password2 = "12345";   // No uppercase letter

            // Act
            bool result1 = _authorizationService.IsPasswordValid(password);
            bool result2 = _authorizationService.IsPasswordValid(password2);

            // Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        [Fact]
        public void SetCurrentWorker_SetsWorkerCorrectly()
        {
            // Arrange
            var worker = new Worker { Id = "1", Name = "Test Worker" };

            // Act
            _authorizationService.SetCurrentWorker(worker);

            // Assert
            Assert.Equal(worker, _authorizationService.CurrentWorker);
        }

        //[Fact]
        //public void IsAdminLoginSuccessful_ValidLogin_ReturnsTrue()
        //{
        //    // Arrange
        //    var admin = new Admin { Username = "admin", Password = "Password123" };
        //    _mockDbContext.AddAdmin(admin); // Use AddAdmin to add the admin

        //    // Act
        //    bool result = _authorizationService.IsAdminLoginSuccessful("Password123", "admin");

        //    // Assert
        //    Assert.True(result);
        //    Assert.NotNull(_authorizationService.CurrentAdmin);
        //}

        //[Fact]
        //public void IsAdminLoginSuccessful_InvalidLogin_ReturnsFalse()
        //{
        //    // Arrange
        //    var admin = new Admin { Username = "admin", Password = "Password123" };
        //    _mockDbContext.AddAdmin(admin); // Use AddAdmin to add the admin

        //    // Act
        //    bool result = _authorizationService.IsAdminLoginSuccessful("WrongPassword", "admin");

        //    // Assert
        //    Assert.False(result);
        //    Assert.Null(_authorizationService.CurrentAdmin);
        //}

        [Fact]
        public void SetCurrentAdmin_SetsAdminCorrectly()
        {
            // Arrange
            var admin = new Admin { Username = "admin", Password = "Password123" };

            // Act
            _authorizationService.SetCurrentAdmin(admin);

            // Assert
            Assert.Equal(admin, _authorizationService.CurrentAdmin);
        }

        [Fact]
        public void SetCurrentAdmin_SetsAdminToNull()
        {
            // Act
            _authorizationService.SetCurrentAdmin(null);

            // Assert
            Assert.Null(_authorizationService.CurrentAdmin);
        }
    }

    // Mock DbContext class for testing purposes
    public class MockManagementSystemDbContext
    {
        private readonly List<Admin> _administrators;

        public MockManagementSystemDbContext()
        {
            _administrators = new List<Admin>();
            Administrators = _administrators.AsQueryable().BuildMockDbSet();
        }

        public IQueryable<Admin> Administrators { get; }

        // Method to simulate adding administrators
        public void AddAdmin(Admin admin)
        {
            _administrators.Add(admin);
        }
    }

    public class MockCompanyService : ICompanyService
    {
        public void SetCurrentCompanyAsAdmin(Admin admin)
        { }

        public void SetCurrentCompany(string companyName)
        { }

        public void SetCurrentCompanyByCompanyId(Company company)
        { }

        public Company GetCurrentCompany(string id)
        { return default!; }

        public Company CurrentCompany { get; set; }
    }

    // Mock extension for IQueryable to mock DbSet methods
    public static class MockDbSetExtensions
    {
        public static IQueryable<T> BuildMockDbSet<T>(this IQueryable<T> source) where T : class
        {
            var mockSet = new Mock<IQueryable<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(source.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(source.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(source.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(source.GetEnumerator());
            return source;
        }
    }
}
