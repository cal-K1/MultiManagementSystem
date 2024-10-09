using MultiManagementSystem.Data;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services
{
    public class ApplicationService(ManagementSystemDbContext dbContext) : IApplicationService
    {
        public JobApplication GetApplication(string id)
        {
            var application = dbContext.JobApplication.FirstOrDefault(jobApplication => jobApplication.Id == id);
            if (application == null)
            {
                throw new InvalidOperationException($"Job application with ID {id} not found.");
            }
            return application;
        }

        public List<JobApplication> GetAllPendingJobApplications()
        {
            List<JobApplication> allJobApplications = dbContext.JobApplication
                .Where(application => application.ApplicationState == ApplicationState.Pending)
                .ToList();

            return allJobApplications;
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
