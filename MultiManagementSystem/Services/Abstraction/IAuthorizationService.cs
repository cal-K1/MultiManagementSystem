using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IAuthorizationService
{
    public Worker CurrentWorker { get; }
    public Admin CurrentAdmin { get; }

    /// <summary>
    /// Checks if the username meets the required criteria.
    /// </summary>
    /// <returns>True if the username is valid, otherwise false.</returns>
    bool IsUserNameValid(string userName);

    /// <summary>
    /// Checks if the provided password meets the required criteria.
    /// </summary>
    /// <returns>True if the password is valid, otherwise false.</returns>
    bool IsPasswordValid(string password);

    /// <summary>
    /// Sets the current worker object.
    /// </summary>
    void SetCurrentWorker(Worker worker);

    /// <summary>
    /// Checks if the provided admin username and password match.
    /// </summary>
    /// <returns>True if the login is successful, otherwise false.</returns>
    bool IsAdminLoginSuccessful(string username, string password);

    /// <summary>
    /// Sets the current admin object.
    /// </summary>
    void SetCurrentAdmin(Admin admin);
}
