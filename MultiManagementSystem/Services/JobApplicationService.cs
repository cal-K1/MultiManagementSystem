using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services
{
    public class ApplicationService(ManagementSystemDbContext dbContext) : IApplicationService
    {
        public JobApplication GetApplication(string Id)
        {
            var application = dbContext.JobApplications.FirstOrDefault(jobApplication => jobApplication.Id == Id);
            if (application == null)
            {
                throw new InvalidOperationException($"Job application with ID {Id} not found.");
            }
            return application;
        }
    }
}
