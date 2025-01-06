using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IDatabaseService
{
    /// <summary>
    /// Checks if the provided login credentials are correct.
    /// </summary>
    /// <returns>True if the login is successful, otherwise false.</returns>
    Task<bool> SetUserLoggedIn(string enteredPassword, string workerNumber);

    /// <summary>
    /// Retrieves a worker object based on the worker number.
    /// </summary>
    /// <returns>The worker associated with the given worker number.</returns>
    Task<Worker> GetWorkerById(string id);

    /// <summary>
    /// Creates a new admin with the specified username and password and saves it to the database.
    /// </summary>
    Task CreateAdmin(string username, string password);

    /// <summary>
    /// Retrieves all job roles associated with the provided company ID.
    /// </summary>
    /// <returns>A list of job roles for the given company.</returns>
    Task<List<JobRole>> GetAllJobRolesByCompanyId(string companyId);

    /// <summary>
    /// Creates a new company and saves it to the database.
    /// </summary>
    Task CreateCompany(Company newCompany);

    /// <summary>
    /// Gets all pending job applications.
    /// </summary>
    /// <returns>A list of all pending job applications.</returns>
    Task<List<JobApplication>> GetAllPendingJobApplications();

    /// <summary>
    /// Gets all leave requests from the given company.
    /// </summary>
    /// <returns>A list of all pending job applications.</returns>
    Task<List<LeaveRequest>> GetAllPendingLeaveRequestsByCompanyId(string companyId);

    /// <summary>
    /// Declines the job application.
    /// </summary>
    Task DeclineApplication(JobApplication application);

    /// <summary>
    /// Accepts the job application.
    /// </summary>
    Task AcceptApplication(JobApplication application);

    /// <summary>
    /// Applies for a job using the applicant details and application text.
    /// </summary>
    Task ApplyJob(string applicantId, string name, string phoneNumber, string applicationText);

    /// <summary>
    /// Adds a new leave request for the given worker and saves it in the database.
    /// </summary>
    Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest);

    /// <summary>
    /// Gets a worker object based on the provided worker number.
    /// </summary>
    /// <returns>The worker associated with the given worker number.</returns>
    Task<Worker> GetWorkerByWorkerNumber(string workerNumber);

    /// <summary>
    /// Creates a new worker and saves it in the database.
    /// </summary>
    Task CreateNewWorkerInDb(Worker worker);

    /// <summary>
    /// Gets all workers from a given country and companyId.
    /// </summary>
    /// <returns>A list of workers that have a country property matching the inputted country.</returns>
    Task<List<Worker>> GetWorkersByCountry(string companyid, WorkerCountry country);

    /// <summary>
    /// Removes the given worker from the database.
    /// </summary>
    void RemoveWorker(Worker worker);

    /// <summary>
    /// Sets the worker's job role to the given job role and saves the changes to the database.
    /// </summary>
    Task SaveJobRoleToWorker(Worker worker, string jobRoleId);

    /// <summary>
    /// Gets a list of all workers in the database with the given company ID.
    /// </summary>
    /// <returns>A list of workers for the given company ID.</returns>
    Task<List<Worker>> GetWorkersByCompanyId(string companyId);

    /// <summary>
    /// Adds a new job role to the database and saves it.
    /// </summary>
    Task AddNewJobRole(JobRole jobRole);

    /// <summary>
    /// Saves a new notification message to the worker's notification list.
    /// </summary>
    Task SaveNewNotification(Worker worker, Notification notification);

    /// <summary>
    /// Saves a new notification message to the worker's notification list.
    /// </summary>
    Task<List<Notification>> GetWorkerNotifications(Worker worker);

    /// <summary>
    /// Clears all notifications from the worker's notification list.
    /// </summary>
    Task ClearWorkerNotifications(Worker worker);

    /// <summary>
    /// Clears given notification from the worker's notification list.
    /// </summary>
    Task RemoveWorkerNotification(Worker worker, Notification notification);

    /// <summary>
    /// Accepts the passed in leave request in the db.
    /// </summary>
    Task HandleLeaveRequestInDatabase(LeaveRequest leaveRequest, bool accepted);
}
