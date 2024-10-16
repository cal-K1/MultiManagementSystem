﻿using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class WorkerService(ManagementSystemDbContext dbContext) : IWorkerService
{
    public async Task<Worker> GetWorker(string workerId)
    {
        // First, try to find an EmployedWorker with the given ID
        var employedWorker = await dbContext.EmployedWorker
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

        // If neither is found, return null or throw an exception
        return null;
    }


    public int GetWorkerLeaveDaysRemaining(string WorkerId)
    {
        var employedWorker = dbContext.EmployedWorker.FirstOrDefault(employedWorker => employedWorker.Id == WorkerId);
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