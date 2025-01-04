using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services
{
    public class ApplicationService(ManagementSystemDbContext dbContext) : IApplicationService
    {
        /// <summary>
        /// Returns a job application that matches the Id passed in.
        /// </summary>
        /// <returns>A job application in the database with the specified Id.</returns>
        public JobApplication GetApplication(string Id)
        {
            var application = dbContext.JobApplications.FirstOrDefault(jobApplication => jobApplication.Id == Id);
            if (application == null)
            {
                throw new InvalidOperationException($"Job application with ID {Id} not found.");
            }
            return application;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A list of all job applications in the database that have ApplicationState as 'Pending'.</returns>
        //public async Task<List<JobApplication>> GetAllPendingJobApplications()
        //{
        //    List<JobApplication> allJobApplications = await dbContext.JobApplications
        //        .Where(application => application.ApplicationState == ApplicationState.Pending)
        //        .ToListAsync();

        //    return allJobApplications;
        //}

        /// <summary>
        /// Accepts a job application and saves the changes in the database.
        /// </summary>
        //public async Task AcceptApplication(JobApplication application)
        //{
        //    application.ApplicationState = ApplicationState.Accepted;
        //    await dbContext.SaveChangesAsync();
        //}

        /// <summary>
        /// Declines a job application and saves the changes in the database.
        /// </summary>
        //public async Task DeclineApplication(JobApplication application)
        //{
        //    application.ApplicationState = ApplicationState.Declined;
        //    await dbContext.SaveChangesAsync();
        //}

        /// <summary>
        /// Creates a job application and saves it in the database.
        /// </summary>
        //public async Task ApplyJob(string applicantId,string name, string phoneNumber, string applicationText)
        //{
        //    JobApplication newApplication = new JobApplication
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        ApplicantId = applicantId,
        //        ApplicantName = name,
        //        ApplicantPhoneNumber = phoneNumber,
        //        ApplicationText = applicationText,
        //        ApplicationState = ApplicationState.Pending
        //    };

        //    await dbContext.JobApplications.AddAsync(newApplication);
        //    await dbContext.SaveChangesAsync();
        //}
    }
}
