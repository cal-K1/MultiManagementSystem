using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IAuthorizationService
{
    public Worker CurrentWorker { get; }
    public Admin CurrentAdmin { get; }

    bool IsUserNameValid(string userName);
    bool IsPasswordValid(string password);
    Task<bool> IsLoginSuccessful(string enteredPassword, string workerNumber);
    Task<Worker> GetWorkerFromWorkerNumber(string workerNumber);
    bool IsAdminLoginSuccessful(string username, string password);
    void SetCurrentAdmin(Admin admin);
}
