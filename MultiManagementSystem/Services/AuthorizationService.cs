using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;

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


    public async Task<bool> IsLoginSuccessful(string enteredPassword, string workerNumber)
    {
        using var scope = serviceProvider.CreateScope();
        var workerService = scope.ServiceProvider.GetRequiredService<IWorkerService>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

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

            if (isWorkerLoginSuccessfull)
            {
                CurrentWorker = await workerService.GetWorkerByWorkerNumber(workerNumber);
                if (CurrentWorker != null)
                {
                    companyService.GetCurrentCompany(CurrentWorker.CompanyId);
                }
            }

            CurrentAdmin = null;
            return isWorkerLoginSuccessfull;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public async Task<Worker> GetWorkerFromWorkerNumber(string workerNumber)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        // Check if the worker exists in Workers.
        var worker = await dbContext.Workers
            .FirstOrDefaultAsync(worker => worker.WorkerNumber == workerNumber);

        if (worker != null)
        {
            return worker;
        }
        else
        {
            return null!;
        }
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
