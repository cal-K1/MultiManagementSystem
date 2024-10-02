using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services.Abstraction;

public interface IApplicationService
{
    void GetApplication();

    void DeclineApplication(Application application);

    void AcceptApplication(Application application);

    Task ApplyJob(string name, string phoneNumber, string applicationText);
}
