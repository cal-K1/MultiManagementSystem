using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class WorkerService(ManagementSystemDbContext dbContext) : IWorkerService
{
    public async Task<Worker> GetWorker(string workerId)
    {
        // First, try to find an EmployedWorker with the given ID
        var employedWorker = await dbContext.EmployedWorkers
                                           .FirstOrDefaultAsync(e => e.Id == workerId);
        if (employedWorker != null)
        {
            return employedWorker;
        }

        // If not found, try to find a ContractWorker with the given ID
        var contractWorker = await dbContext.ContractWorkers
                                           .FirstOrDefaultAsync(c => c.Id == workerId);
        if (contractWorker != null)
        {
            return contractWorker;
        }

        return null!;
    }

    public int GetWorkerLeaveDaysRemaining(string WorkerId)
    {
        var employedWorker = dbContext.EmployedWorkers.FirstOrDefault(employedWorker => employedWorker.Id == WorkerId);
        if (employedWorker == null)
        {
            var contractWorker = dbContext.ContractWorkers.FirstOrDefault(contractWorker => contractWorker.Id == WorkerId);
            if (contractWorker == null)
            {
                throw new InvalidOperationException($"Worker with ID {WorkerId} not found.");
            }
            return contractWorker.LeaveDaysRemaining;
        }

        return employedWorker.LeaveDaysRemaining;
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

    public async Task CreateNewEmployedWorker(string name, string password, EmployeeType employeeType)
    {
        EmployedWorker employedWorker = new()
        { 
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Password = password,
            EmployeeType = employeeType,
            WorkerNumber = CreateNewWorkerNumber(),
            LeaveDaysRemaining = 25,

        };

        await dbContext.EmployedWorkers.AddAsync(employedWorker);
        await dbContext.SaveChangesAsync();
    }

    public async Task CreateNewContractWorker(string name, string password)
    {
        ContractWorker contractWorker = new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Password = password,
            WorkerNumber = CreateNewWorkerNumber(),
            LeaveDaysRemaining = 25,
        };

        await dbContext.ContractWorkers.AddAsync(contractWorker);
        await dbContext.SaveChangesAsync();
    }
}
