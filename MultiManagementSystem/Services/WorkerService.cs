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
        // Generate a random letter and a 6 digit number.
        char randomLetter = (char)new Random().Next('A', 'Z' + 1);
        int randomNumber = new Random().Next(0, 1000000);

        // Combine the letter and number into the worker number format
        string workerNumber = $"{randomLetter}{randomNumber:D6}";

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
}
