﻿using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    Task<Worker> GetWorkerByWorkerNumber(string workerNumber);
    int GetWorkerLeaveDaysRemaining(string WorkerId);
    public string CreateNewWorkerNumber();
    Task CreateNewWorkerInDb(Worker worker);
    List<Worker> GetWorkersByCountry(WorkerCountry country);
    Task SaveJobRoleToWorker(Worker worker, string jobRoleId);
    Task AddNewJobRole(JobRole jobRole);
}
