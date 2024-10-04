using MultiManagementSystem.Data;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services
{
    public class ApplicationService(ManagementSystemDbContext dbContext) : IApplicationService
    {
        public JobApplication GetApplication(string Id)
        {
            foreach (JobApplication jobApplication in dbContext.JobApplication)
            {
                if (jobApplication.Id == Id)
                {
                    return jobApplication;
                }
            }

            return null!;
        }
        public async Task AcceptApplication(JobApplication application)
        {
            application.ApplicationState = ApplicationState.Accepted;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeclineApplication(JobApplication application)
        {
            application.ApplicationState = ApplicationState.Declined;
            await dbContext.SaveChangesAsync();
        }

        public async Task ApplyJob(string name, string phoneNumber, string applicationText)
        {
            JobApplication newApplication = new JobApplication
            {
                Id = Guid.NewGuid().ToString(),
                ApplicantName = name,
                ApplicantPhoneNumber = phoneNumber,
                ApplicationText = applicationText,
                ApplicationState = ApplicationState.Pending
            };

            await dbContext.JobApplication.AddAsync(newApplication);
            await dbContext.SaveChangesAsync();
        }
    }
}
