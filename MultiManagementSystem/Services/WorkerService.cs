using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class WorkerService(ManagementSystemDbContext dbContext, ICompanyService companyService) : IWorkerService
{
    public async Task<Worker> GetWorkerByWorkerNumber(string workerNumber)
    {
        // First, try to find an Worker with the given ID
        return await dbContext.Workers.FirstOrDefaultAsync(e => e.WorkerNumber == workerNumber);
    }

    public async Task<List<Worker>> GetWorkersByCompanyId(string companyId)
    {
        // Return all workers with the given company ID.
        return await dbContext.Workers.Where(e => e.CompanyId == companyId).ToListAsync();
    }

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

    private bool IsWorkerNumberAlreadyInUse(string workerNumber)
    {
        if (dbContext.Workers.Any(w => w.WorkerNumber == workerNumber))
        {
            return true;
        }

        return false;
    }

    public List<Worker> GetWorkersByCountry(WorkerCountry country) => dbContext.Workers.Where(worker => worker.Country == country).ToList();


    public async Task SaveJobRoleToWorker(Worker worker, string jobRole)
    {
        if (worker == null || jobRole == null)
        {
            throw new Exception(message: $"Worker or JobRole is null.");
        }

        worker.JobRoleId = jobRole;

        dbContext.Workers.Update(worker);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddNewJobRole(JobRole jobRole)
    {
        jobRole.Id = Guid.NewGuid().ToString();

        await dbContext.JobRole.AddAsync(jobRole);
        await dbContext.SaveChangesAsync();
    }
}
