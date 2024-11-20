using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IAuthorizationService
{
    public Worker CurrentWorker { get; }

    bool IsUserNameValid(string userName);
    bool IsPasswordValid(string password);
    Task<bool> IsLoginSuccessful(string enteredPassword, string workerNumber);
    Task<Worker> GetWorkerFromWorkerNumber(string workerNumber);
}
