using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services.Abstraction;

public interface IApplicationService
{
    JobApplication GetApplication(string Id);

    Task DeclineApplication(JobApplication application);

    Task AcceptApplication(JobApplication application);

    Task ApplyJob(string name, string phoneNumber, string applicationText);
}
