using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class AssignJobRolePage
{
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;
    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;
    [Inject]
    private LogFactory LogFactory { get; set; } = default!;

    public string ManagerWorkerNumber { get; set; } = string.Empty;
    public string ManagerPassword { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsManagerAuthorized { get; set; } = false;
    public bool Submitted { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;
    public bool ShowErrorMessage { get; set; } = false;
    public string WorkerJobTitle { get; set; } = string.Empty;
    public int WorkerSalary { get; set; } = 0;
    public string WorkerDescription { get; set; } = string.Empty;
    public string SelectedWorkerId { get; set; } = string.Empty;
    public Worker SelectedWorker { get; set; } = default!;
    public List<Worker> Workers { get; set; } = new();
    public List<JobRole> JobRoles { get; set; } = new();
    public bool CreateNewRole { get; set; } = false;
    public string SelectedJobRoleId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        Workers = await GetListOfRelevantWorkers();
        JobRoles = await databaseService.GetAllJobRolesByCompanyId(authorizationService.CurrentWorker.CompanyId);
    }

    private async void CheckAuthorization()
    {
        Workers = await GetListOfRelevantWorkers();

        if (await databaseService.SetUserLoggedIn(ManagerPassword, ManagerWorkerNumber)
            && (await databaseService.GetWorkerByWorkerNumber(ManagerWorkerNumber)).Manager)
        {
            IsManagerAuthorized = true;

            var logger = LogFactory.CreateLogger("DatabaseService", LoggerType.ConsoleLogger);
            logger.Info($"Manager authorized: {ManagerWorkerNumber}");
        }
        else
        {
            ErrorMessage = "Invalid Manager Login";
            ShowErrorMessage = true;
        }
    }

    private async Task<List<Worker>> GetListOfRelevantWorkers()
    {
        if (authorizationService.CurrentWorker == null)
        {
            IsManagerAuthorized = false;
            return new();
        }
        else
        {
            return await databaseService.GetWorkersByCompanyId(authorizationService.CurrentWorker.CompanyId) ?? new();
        }
    }

    private void AssignJob()
    {
        try
        {
            if (SelectedWorker == null)
            {
                var logger = LogFactory.CreateLogger("DatabaseService", LoggerType.ConsoleLogger);
                logger.Error("No Worker Selected");

                ErrorMessage = "Worker not found. Please select a valid worker.";
                ShowErrorMessage= true;
                return;
            }

            if (!CreateNewRole)
            {
                // Assign an existing job role.
                if (string.IsNullOrEmpty(SelectedJobRoleId))
                {
                    ErrorMessage = "Job Role not Selected. Please try again.";
                    ShowErrorMessage = true;

                    return;
                }

                databaseService.SaveJobRoleToWorker(SelectedWorker, SelectedJobRoleId);

                var logger = LogFactory.CreateLogger("DatabaseService", LoggerType.ConsoleLogger);
                logger.Error($"JobRole saved to worker: {SelectedWorker.WorkerNumber}");
            }
            else
            {
                // Create and assign a new job role
                JobRole jobRole = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    JobTitle = WorkerJobTitle,
                    Salary = WorkerSalary,
                    Description = WorkerDescription
                };

                databaseService.AddNewJobRole(jobRole);
                databaseService.SaveJobRoleToWorker(SelectedWorker, jobRole.Id);

                var logger = LogFactory.CreateLogger("DatabaseService", LoggerType.ConsoleLogger);
                logger.Error($"JobRole saved to worker: {SelectedWorker.WorkerNumber}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error Saving Job Role - {ex.Message}";
            ShowErrorMessage = true;

            var logger = LogFactory.CreateLogger("DatabaseService", LoggerType.ConsoleLogger);
            logger.Error($"JobRole saved to worker: {SelectedWorker.WorkerNumber}");
        }

        Submitted = true;
    }

    private void SwitchToCreateNewRole()
    {
        CreateNewRole = true;
    }

    private void SwitchToExistingRole()
    {
        CreateNewRole = false;
    }
}
