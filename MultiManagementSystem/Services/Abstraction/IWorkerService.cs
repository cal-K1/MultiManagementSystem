using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    Task<Worker> GetWorker(string workerId);
    int GetWorkerLeaveDaysRemaining(string WorkerId);
    public string CreateNewWorkerNumber();
    Task CreateNewWorker(string name, string password);

}
