using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    EmployedWorker GetEmployedWorker(string employedWorkerId);
    ContractWorker GetContractWorker(string contractWorkerId);
    int GetWorkerLeaveDaysRemaining(string WorkerId);
}
