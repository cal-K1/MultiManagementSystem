using MultiManagementSystem.Models;

public interface IJobApplicationStrategy
{
    Task<string> ValidateApplication(JobApplication jobApplication);
}
