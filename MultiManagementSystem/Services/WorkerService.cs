using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class WorkerService(ManagementSystemDbContext dbContext) : IWorkerService
{
    public async Task<Worker> GetWorker(string workerId)
    {
        // First, try to find an Worker with the given ID
        var worker = await dbContext.Workers.FirstOrDefaultAsync(e => e.Id == workerId);
        if (worker != null)
        {
            return worker;
        }

        return null!;
    }

    public int GetWorkerLeaveDaysRemaining(string workerId)
    {
        // Retrieve the worker by their Id (foreign key to UserId)
        var worker = dbContext.Workers.FirstOrDefault(w => w.Id == workerId);
        var user = dbContext.UserId.FirstOrDefault(u => u.Id == worker.Id);

        // Retrieve LeaveDaysRemaining from the UserId table where UserId.Id matches Worker.Id
        if (worker == null || user == null)
        {
            throw new InvalidOperationException($"Worker or User with ID {workerId} not found.");
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


    public async Task CreateNewWorker(string name, string password, string workerNumber)
    {
        Worker worker = new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Password = password,
            WorkerNumber = workerNumber,
        };

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
}
