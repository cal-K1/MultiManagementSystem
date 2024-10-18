using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public class AuthorizationService(ManagementSystemDbContext dbContext) : IAuthorizationService
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    public bool IsUserNameValid(string userName) => !string.IsNullOrEmpty(userName) && userName.Length > 1;

    public bool IsPasswordValid(string password)
    {
        string pattern = @"^(?=.*[A-Z])(?=.*\d).{6,}$";

        if (password != null && System.Text.RegularExpressions.Regex.IsMatch(password, pattern))
        {
            return true;
        }

        return false;
    }

    public bool IsLoginSuccessful(string enteredPassword, string workerNumber)
    {
        return dbContext.EmployedWorkers.Any(worker => worker.Password == enteredPassword && worker.WorkerNumber == workerNumber) ||
               dbContext.ContractWorkers.Any(worker => worker.Password == enteredPassword && worker.WorkerNumber == workerNumber);
    }
}
