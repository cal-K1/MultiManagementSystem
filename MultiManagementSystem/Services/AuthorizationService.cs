using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public class AuthorizationService(ManagementSystemDbContext dbContext) : IAuthorizationService
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    /// <summary>
    /// Checks if the username is of the correct length (between 2 and 60 characters.
    /// </summary>
    /// <returns>true if the username is valid.</returns>
    public bool IsUserNameValid(string userName) => !string.IsNullOrEmpty(userName) && userName.Length > 1 && userName.Length < 61;

    /// <summary>
    /// Checks if the password meets the requirements.
    /// </summary>
    /// <returns>true if the password passes the password requirements.</returns>
    public bool IsPasswordValid(string password)
    {
        string pattern = @"^(?=.*[A-Z])(?=.*\d).{6,}$";

        if (password != null && System.Text.RegularExpressions.Regex.IsMatch(password, pattern))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the workerNumber and password match a worker in the db.
    /// </summary>
    /// <returns>true if login is successful.</returns>
    public bool IsLoginSuccessful(string enteredPassword, string workerNumber)
    {
        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(enteredPassword) ||
                string.IsNullOrWhiteSpace(workerNumber) ||
                dbContext.Workers == null)
            {
                return false;
            }

            var allWorkers = dbContext.Workers.ToList();

            bool isWorkerLoginSuccessfull = allWorkers.Any(worker =>
                worker.Password == enteredPassword && worker.WorkerNumber == workerNumber);

            return isWorkerLoginSuccessfull;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    /// <summary>
    /// Gets the worker with the specified Id in the database.
    /// </summary>
    /// <returns>The worker with the specified Id in the database.</returns>
    public async Task<Worker> GetWorkerFromWorkerNumber(string workerNumber)
    {
        // Check if the worker exists in Workers.
        var worker = await dbContext.Workers
            .FirstOrDefaultAsync(worker => worker.WorkerNumber == workerNumber);

        if (worker != null)
        {
            // Return if found in Workers.
            return worker;
        }
        else
        {
            return null!;
        }
    }
}
