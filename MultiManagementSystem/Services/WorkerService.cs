using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class WorkerService(ManagementSystemDbContext dbContext) : IWorkerService
{
    public EmployedWorker GetEmployedWorker(string employedWorkerId)
    {
        var employedWorker = dbContext.EmployeedWorker.FirstOrDefault(employedWorker => employedWorker.Id == employedWorkerId);
        if (employedWorker == null)
        {
            return null!;
        }

        return employedWorker;
    }

    public ContractWorker GetContractWorker(string contractWorkerId)
    {
        var contractWorker = dbContext.ContractWorkers.FirstOrDefault(contractWorker => contractWorker.Id == contractWorkerId);
        if (contractWorker == null)
        {
            return null!;
        }

        return contractWorker;
    }

    public int GetWorkerLeaveDaysRemaining(string WorkerId)
    {
        var employedWorker = dbContext.EmployeedWorker.FirstOrDefault(employedWorker => employedWorker.Id == WorkerId);
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
}
