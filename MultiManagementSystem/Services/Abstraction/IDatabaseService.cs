using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IDatabaseService
{
    Task<bool> IsLoginSuccessful(string enteredPassword, string workerNumber);
    Task<Worker> GetWorkerFromWorkerNumber(string workerNumber);
    Task CreateAdmin(string username, string password);
    Task<List<JobRole>> GetAllJobRolesByCompanyId(string companyId);
    Task CreateCompany(Company newCompany);
    Task<List<JobApplication>> GetAllPendingJobApplications();
    Task DeclineApplication(JobApplication application);
    Task AcceptApplication(JobApplication application);
    Task ApplyJob(string applicantId, string name, string phoneNumber, string applicationText);
    Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest);
    /// <summary>
    /// Gets the worker from the db with the given workerNumber.
    /// </summary>
    /// <returns>The worker with given worker number.</returns>
    Task<Worker> GetWorkerByWorkerNumber(string workerNumber);
    /// <summary>
    /// Creates a worker with the specified properties passed in as parameters and saves it in the database.
    /// </summary>
    Task CreateNewWorkerInDb(Worker worker);

    /// <summary>
    /// Gets all workers from a given country.
    /// </summary>
    /// <returns>A list of Workers that have a Country property that matches the inputted country.</returns>
    List<Worker> GetWorkersByCountry(WorkerCountry country);

    /// <summary>
    /// Sets the workers JobRole property to the given jobRole and saves the changes to the database.
    /// </summary>
    Task SaveJobRoleToWorker(Worker worker, string jobRoleId);

    /// <summary>
    /// Gets a list of all workers in the database with the given company id.
    /// </summary>
    Task<List<Worker>> GetWorkersByCompanyId(string companyId);

    /// <summary>
    /// Adds a new JobRole in the database and saves it.
    /// </summary>
    Task AddNewJobRole(JobRole jobRole);

    /// <summary>
    /// Saves the notification to the Notifications list of the worker.
    /// </summary>
    Task SaveNewNotification(Worker Worker, string NotificationMessage);

    /// <summary>
    /// Clears all notifications from the worker's Notifications list.
    /// </summary>
    Task ClearWorkerNotifications(Worker Worker);
}
