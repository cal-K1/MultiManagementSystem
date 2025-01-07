using MultiManagementSystem.Models;

public class FullTimeJobStrategy : IJobApplicationStrategy
{
    public async Task<string> ValidateApplication(JobApplication jobApplication)
    {
        if (string.IsNullOrWhiteSpace(jobApplication.ApplicantName))
        {
            return "Full-time applicant's name is required.";
        }
        if (string.IsNullOrWhiteSpace(jobApplication.ApplicantPhoneNumber))
        {
            return "Full-time applicant's phone number is required.";
        }
        if (string.IsNullOrWhiteSpace(jobApplication.ApplicationText))
        {
            return "Full-time applicant's application text is required.";
        }
        if (string.IsNullOrWhiteSpace(jobApplication.JobRole.ToString()))
        {
            return "Full-time applicant must have a jobrole selected.";
        }

        return "Application valid for full-time job.";
    }
}
