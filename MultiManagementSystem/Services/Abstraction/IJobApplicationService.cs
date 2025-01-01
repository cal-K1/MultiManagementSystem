using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services.Abstraction;

public interface IApplicationService
{
    /// <summary>
    /// Returns a job application that matches the Id passed in.
    /// </summary>
    /// <returns>A job application in the database with the specified Id.</returns>
    JobApplication GetApplication(string Id);

    /// <summary>
    /// Gets all the job applications associated with that company.
    /// </summary>
    /// <returns>A list of all job applications in the database that have ApplicationState as 'Pending'.</returns>
    Task<List<JobApplication>> GetAllPendingJobApplications();

    /// <summary>
    /// Rejects a job application and saves the changes in the database.
    /// </summary>
    Task DeclineApplication(JobApplication application);

    /// <summary>
    /// Accepts a job application and saves the changes in the database.
    /// </summary>
    Task AcceptApplication(JobApplication application);

    /// <summary>
    /// Creates a job application and saves it in the database.
    /// </summary>
    Task ApplyJob(string name, string phoneNumber, string applicationText);
}
