using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public class AuthorizationService(IServiceProvider serviceProvider, ICompanyService companyService) : IAuthorizationService
{
    public Worker CurrentWorker { get; private set; }
    public Admin? CurrentAdmin { get; private set; }

    public bool IsUserNameValid(string userName) => !string.IsNullOrEmpty(userName) && userName.Length > 1 && userName.Length < 61;

    public bool IsPasswordValid(string password)
    {
        string pattern = @"^(?=.*[A-Z])(?=.*\d).{6,}$";

        if (password != null && System.Text.RegularExpressions.Regex.IsMatch(password, pattern))
        {
            return true;
        }

        return false;
    }

    public void SetCurrentWorker(Worker worker)
    {
        CurrentWorker = worker;
    }

    public bool IsAdminLoginSuccessful(string enteredPassword, string adminUsername)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        if (dbContext.Administrator.Any(admin => admin.Password == enteredPassword && admin.Username == adminUsername))
        {
            CurrentAdmin = dbContext.Administrator.FirstOrDefault(admin => admin.Username == adminUsername);
            if (CurrentAdmin == null)
            {
                return false;
            }

            companyService.SetCurrentCompanyAsAdmin(CurrentAdmin);
            return true;
        }

        return false;
    }

    public void SetCurrentAdmin(Admin admin)
    {
        CurrentAdmin = admin ?? null;
    }
}
