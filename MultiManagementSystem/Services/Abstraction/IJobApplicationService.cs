using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services.Abstraction;

public interface IApplicationService
{
    JobApplication GetApplication(string Id);

    //Task<List<JobApplication>> GetAllPendingJobApplications();

    //Task DeclineApplication(JobApplication application);

    //Task AcceptApplication(JobApplication application);

    //Task ApplyJob(string applicantId,string name, string phoneNumber, string applicationText);
}
