using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IAuthorizationService
{
    public Worker CurrentWorker { get; }
    public Admin CurrentAdmin { get; }

    /// <summary>
    /// Checks if the username is of the correct length (between 2 and 60 characters.
    /// </summary>
    /// <returns>true if the username is valid.</returns>
    bool IsUserNameValid(string userName);

    /// <summary>
    /// Checks if the password meets the requirements.
    /// </summary>
    /// <returns>true if the password passes the password requirements.</returns>
    bool IsPasswordValid(string password);

    /// <summary>
    /// Checks if the workerNumber and password match a worker in the db.
    /// </summary>
    /// <returns>true if login is successful.</returns>
    Task<bool> IsLoginSuccessful(string enteredPassword, string workerNumber);

    /// <summary>
    /// Gets the worker with the specified Id in the database.
    /// </summary>
    /// <returns>The worker with the specified Id in the database.</returns>
    Task<Worker> GetWorkerFromWorkerNumber(string workerNumber);

    /// <summary>
    /// Checks if the admin username and password match a worker in the db.
    /// </summary>
    /// <returns>true if login is successful.</returns>
    bool IsAdminLoginSuccessful(string username, string password);

    /// <summary>
    /// Sets the CurrentAdmin as the admin passed in.
    /// </summary>
    void SetCurrentAdmin(Admin admin);
}
