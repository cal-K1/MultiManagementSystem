using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class WorkerService(ManagementSystemDbContext dbContext, ICompanyService companyService) : IWorkerService
{
    /// <summary>
    /// Gets the worker from the db with the given workerNumber.
    /// </summary>
    /// <param name="workerId"></param>
    /// <returns>The worker with given worker number.</returns>
    public async Task<Worker> GetWorkerByWorkerNumber(string workerNumber)
    {
        // First, try to find an Worker with the given ID
        var worker = await dbContext.Workers.FirstOrDefaultAsync(e => e.WorkerNumber == workerNumber);
        if (worker != null)
        {
            return worker;
        }

        return null!;
    }

    /// <summary>
    /// Gets the number of leave days remaining for the worker with the given worker number.
    /// </summary>
    /// <returns>The number of leave days remaining for a given worker.</returns>
    /// <exception cref="InvalidOperationException"></exception
    public int GetWorkerLeaveDaysRemaining(string workerId)
    {
        var worker = dbContext.Workers.FirstOrDefault(w => w.Id == workerId);
        var user = dbContext.UserId.FirstOrDefault(u => u.Id == worker.Id);

        if (worker == null)
        {
            return -1;
        }

        return user.LeaveDaysRemaining;
    }

    /// <summary>
    /// Creates a unique worker number.
    /// </summary>
    /// <returns>A new worker number as a string.</returns>
    public string CreateNewWorkerNumber()
    {
        string workerNumber = string.Empty;
        bool isWorkerNumberUnique = false;

        var random = new Random();

        while (!isWorkerNumberUnique)
        {
            char randomLetter = (char)random.Next('A', 'Z' + 1);
            int randomNumber = random.Next(0, 1000000);

            workerNumber = $"{randomLetter}{randomNumber:D6}";

            if (!IsWorkerNumberAlreadyInUse(workerNumber))
            {
                isWorkerNumberUnique = true;
            }
        }

        return workerNumber;
    }

    /// <summary>
    /// Creates a worker with the specified properties passed in as parameters and saves it in the database.
    /// </summary>
    public async Task CreateNewWorkerInDb(Worker worker)
    {
        if (companyService?.CurrentCompany?.Id == null)
        {
            throw new Exception(message: $"Current company is null.");
        }

        worker.CompanyId = companyService.CurrentCompany.Id;
        await dbContext.Workers.AddAsync(worker);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if the worker number is already present in the database.
    /// </summary>
    /// <returns>true if the worker number is already present in the database.</returns>
    private bool IsWorkerNumberAlreadyInUse(string workerNumber)
    {
        if (dbContext.Workers.Any(w => w.WorkerNumber == workerNumber))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets all workers from a given country.
    /// </summary>
    /// <returns>A list of Workers that have a Country property that matches the inputted country.</returns>
    public List<Worker> GetWorkersByCountry(WorkerCountry country) => dbContext.Workers.Where(worker => worker.Country == country).ToList();

    /// <summary>
    /// Sets the workers JobRole property to the given jobRole and saves the changes to the database.
    /// </summary>
    public async Task SaveJobRole(Worker worker, JobRole jobRole)
    {
        if (worker == null || jobRole == null)
        {
            throw new Exception(message: $"Worker or JobRole is null.");
        }

        worker.JobRole = jobRole;

        dbContext.Workers.Update(worker);
        await dbContext.SaveChangesAsync();
    }
}
