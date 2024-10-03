using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services.Abstraction;

public interface IApplicationService
{
    void GetApplication();

    void DeclineApplication(JobApplication application);

    void AcceptApplication(JobApplication application);

    Task ApplyJob(string name, string phoneNumber, string applicationText);
}
