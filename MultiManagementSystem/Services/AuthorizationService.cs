using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
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
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(enteredPassword) ||
                string.IsNullOrWhiteSpace(workerNumber) ||
                dbContext.EmployedWorkers == null ||
                dbContext.ContractWorkers == null)
            {
                return false;
            }

            // Check in both Employed and Contract workers for matching credentials
            bool isEmployedWorker = dbContext.EmployedWorkers.Any(worker =>
                worker.Password == enteredPassword && worker.WorkerNumber == workerNumber);

            bool isContractWorker = dbContext.ContractWorkers.Any(worker =>
                worker.Password == enteredPassword && worker.WorkerNumber == workerNumber);

            return isEmployedWorker || isContractWorker;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public async Task<Worker> GetWorkerFromWorkerNumber(string workerNumber)
    {
        // Check if the worker exists in EmployedWorkers
        var employedWorker = await dbContext.EmployedWorkers
            .FirstOrDefaultAsync(worker => worker.WorkerNumber == workerNumber);

        if (employedWorker != null)
        {
            return employedWorker; // Return if found in EmployedWorkers
        }

        // Check if the worker exists in ContractWorkers
        var contractWorker = await dbContext.ContractWorkers
            .FirstOrDefaultAsync(worker => worker.WorkerNumber == workerNumber);

        return contractWorker; // Return if found in ContractWorkers (or null if not found in either)
    }
}
