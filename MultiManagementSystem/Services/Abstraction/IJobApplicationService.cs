using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services.Abstraction;

public interface IApplicationService
{
    /// <summary>
    /// Gets the job application from the database with the matching Id.
    /// </summary>
    /// <returns>Job Application with the matching Id</returns>
    JobApplication GetApplication(string Id);
}
