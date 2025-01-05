using Microsoft.EntityFrameworkCore;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;
using MultiManagementSystem.Services;
using System;
using MultiManagementSystem.Models;
using MultiManagementSystem.Logger;

namespace MultiManagementSystem.Services;

public class DatabaseService(IServiceProvider serviceProvider, ManagementSystemDbContext dbContext, ICompanyService companyService) : IDatabaseService
{
    private readonly ILog _log;

    public async Task<bool> IsLoginSuccessful(string enteredPassword, string workerNumber)
    {
        using var scope = serviceProvider.CreateScope();
        var workerService = scope.ServiceProvider.GetRequiredService<IWorkerService>();
        var authorizationService = scope.ServiceProvider.GetRequiredService<IAuthorizationService>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        try
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(enteredPassword) ||
                string.IsNullOrWhiteSpace(workerNumber) ||
                dbContext.Workers == null)
            {
                return false;
            }

            var allWorkers = dbContext.Workers.ToList();

            bool isWorkerLoginSuccessfull = allWorkers.Any(worker =>
                worker.Password == enteredPassword && worker.WorkerNumber == workerNumber);

            if (isWorkerLoginSuccessfull)
            {
                authorizationService.SetCurrentWorker(await GetWorkerByWorkerNumber(workerNumber));
                if (authorizationService.CurrentWorker != null)
                {
                    companyService.GetCurrentCompany(authorizationService.CurrentWorker.CompanyId);
                }
            }

            authorizationService.SetCurrentAdmin(null!);
            return isWorkerLoginSuccessfull;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }

    public async Task<Worker> GetWorkerById(string id)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        // Check if the worker exists in Workers.
        var worker = await dbContext.Workers
            .FirstOrDefaultAsync(worker => worker.Id == id);

        if (worker != null)
        {
            return worker;
        }
        else
        {
            return null!;
        }
    }

    public async Task CreateAdmin(string adminUsername, string adminPassword)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        if (companyService.CurrentCompany == null)
        {
            throw new InvalidOperationException("No company selected.");
        }

        Admin newAdmin = new Admin
        {
            Username = adminUsername,
            Password = adminPassword,
            Id = Guid.NewGuid().ToString()
        };

        companyService.CurrentCompany.Admin = newAdmin;

        dbContext.Administrator.Add(newAdmin);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<JobRole>> GetAllJobRolesByCompanyId(string companyId)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        return await dbContext.JobRole
            .Where(j => j.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task CreateCompany(Company newCompany)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ManagementSystemDbContext>();

        dbContext.Company.Add(newCompany);

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error saving new company.", ex);
        }

        companyService.SetCurrentCompany(newCompany);
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

    public async Task ApplyJob(string applicantId, string name, string phoneNumber, string applicationText)
    {
        JobApplication newApplication = new JobApplication
        {
            Id = Guid.NewGuid().ToString(),
            ApplicantId = applicantId,
            ApplicantName = name,
            ApplicantPhoneNumber = phoneNumber,
            ApplicationText = applicationText,
            ApplicationState = ApplicationState.Pending
        };

        await dbContext.JobApplications.AddAsync(newApplication);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest)
    {
        await dbContext.LeaveRequests.AddAsync(leaveRequest);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Worker> GetWorkerByWorkerNumber(string workerNumber)
    {
        //_log.Info($"Fetching worker with worker number: {workerNumber}");
        return await dbContext.Workers.FirstOrDefaultAsync(e => e.WorkerNumber == workerNumber);
    }

    public async Task<List<Worker>> GetWorkersByCompanyId(string companyId)
    {
        // Return all workers with the given company ID.
        return await dbContext.Workers.Where(e => e.CompanyId == companyId).ToListAsync();
    }

    public async Task SaveJobRoleToWorker(Worker worker, string jobRole)
    {
        if (worker == null || jobRole == null)
        {
            throw new Exception(message: $"Worker or JobRole is null.");
        }

        worker.JobRoleId = jobRole;

        dbContext.Workers.Update(worker);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddNewJobRole(JobRole jobRole)
    {
        jobRole.Id = Guid.NewGuid().ToString();

        await dbContext.JobRole.AddAsync(jobRole);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveNewNotification(Worker Worker, string NotificationMessage)
    {
        if (Worker == null || NotificationMessage == null)
        {
            throw new Exception(message: $"Worker or NotificationMessage is null.");
        }

        dbContext.Workers.FirstOrDefault(w => w.Id == Worker.Id).Notifications.Add(NotificationMessage);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<string>> GetWorkerNotifications(Worker worker)
    {
        if (string.IsNullOrWhiteSpace(worker.Id))
        {
            throw new Exception("Worker Id is null");
        }

        var dbWorker = await dbContext.Workers.FirstOrDefaultAsync(w => w.Id == worker.Id);
        if (dbWorker == null)
        {
            throw new Exception("Worker not found");
        }

        return dbWorker.Notifications ?? new List<string>();
    }

    public async Task ClearWorkerNotifications(Worker Worker)
    {
        if (Worker == null)
        {
            //Logger.Error($"Worker is null.");
            throw new Exception(message: $"Worker is null.");
        }

        dbContext.Workers.FirstOrDefault(w => w.Id == Worker.Id).Notifications.Clear();
        await dbContext.SaveChangesAsync();
    }

    public async Task CreateNewWorkerInDb(Worker worker)
    {
        if (companyService?.CurrentCompany?.Id == null)
        {
            throw new Exception(message: $"Current company is null.");
        }

        worker.CompanyId = companyService.CurrentCompany.Id;
        await dbContext.Workers.AddAsync(worker);
        await dbContext.SaveChangesAsync();
    }

    public List<Worker> GetWorkersByCountry(WorkerCountry country) => dbContext.Workers.Where(worker => worker.Country == country).ToList();
}
