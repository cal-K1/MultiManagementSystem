using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Services.Abstraction;
using MultiManagementSystem.Logger;

using Microsoft.AspNetCore.Components;

namespace MultiManagementSystem.Services
{
    public class ApplicationService(ManagementSystemDbContext dbContext) : IApplicationService
    {
        [Inject]
        private ILog Logger { get; set; } = default!;

        public JobApplication GetApplication(string Id)
        {
            var application = dbContext.JobApplications.FirstOrDefault(jobApplication => jobApplication.Id == Id);
            if (application == null)
            {
                Logger.Warning($"Job application with ID {Id} not found.");
                throw new InvalidOperationException($"Job application with ID {Id} not found.");
            }
            return application;
        }

        public async Task<List<JobApplication>> GetAllPendingJobApplications()
        {
            List<JobApplication> allJobApplications = await dbContext.JobApplications
                .Where(application => application.ApplicationState == ApplicationState.Pending)
                .ToListAsync();



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

            await dbContext.JobApplications.AddAsync(newApplication);
            await dbContext.SaveChangesAsync();
        }
    }
}
