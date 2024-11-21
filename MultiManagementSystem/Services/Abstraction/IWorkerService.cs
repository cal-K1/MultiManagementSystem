using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    Task<Worker> GetWorkerByWorkerNumber(string workerNumber);
    int GetWorkerLeaveDaysRemaining(string WorkerId);
    public string CreateNewWorkerNumber();
    Task CreateNewWorker(string name, string password, string workerNumber);
    List<Worker> GetWorkersByCountry(WorkerCountry country);
}
