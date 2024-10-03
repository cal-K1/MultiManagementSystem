using MultiManagementSystem.Data;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services
{
    public class ApplicationService(ManagementSystemDbContext dbContext) : IApplicationService
    {
        public void GetApplication()
        {
            return;
        }
        public void AcceptApplication(JobApplication application)
        {
            application.ApplicationState = ApplicationState.Accepted;
            return;
        }

        public void DeclineApplication(JobApplication application)
        {
            application.ApplicationState = ApplicationState.Declined;
            return;
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
