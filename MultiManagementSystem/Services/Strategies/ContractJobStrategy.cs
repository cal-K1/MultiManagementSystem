using MultiManagementSystem.Models;

public class ContractJobStrategy : IJobApplicationStrategy
{
    public async Task<string> ValidateApplication(JobApplication jobApplication)
    {
        if (string.IsNullOrWhiteSpace(jobApplication.ApplicantName))
        {
            return "Contract applicant's name is required.";
        }
        if (string.IsNullOrWhiteSpace(jobApplication.ApplicantPhoneNumber))
        {
            return "Contract applicant's phone number is required.";
        }
        if (string.IsNullOrWhiteSpace(jobApplication.ApplicationText))
        {
            return "Contract applicant's application text is required.";
        }

        return "Application valid for contract job.";
    }
}
