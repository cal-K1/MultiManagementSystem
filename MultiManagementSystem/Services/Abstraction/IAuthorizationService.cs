namespace MultiManagementSystem.Services.Abstraction;

public interface IAuthorizationService
{
    bool IsUserNameValid(string userName);
    bool IsPasswordValid(string password);
    bool IsLoginSuccessful(string enteredPassword, string workerNumber);
}
