using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    Task<Worker> GetWorker(string workerId);
    int GetWorkerLeaveDaysRemaining(string WorkerId);
    Task CreateNewEmployedWorker(string Name, string Password, EmployeeType employeeType);
    Task CreateNewContractWorker(string name, string password);
}
