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
    private IWorkerService workerService { get; set; } = default!;
    [Inject]
    private ICompanyService companyService { get; set; } = default!;

    public string ManagerWorkerNumber { get; set; } = string.Empty;
    public string ManagerPassword { get; set; } = string.Empty;
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
        JobRoles = await companyService.GetAllJobRolesByCompanyId(authorizationService.CurrentWorker.CompanyId);
    }

    private async void CheckAuthorization()
    {
        Workers = await GetListOfRelevantWorkers();

        if (await authorizationService.IsLoginSuccessful(ManagerPassword, ManagerWorkerNumber)
            && (await workerService.GetWorkerByWorkerNumber(ManagerWorkerNumber)).Manager)
        {
            IsManagerAuthorized = true;
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
            return await workerService.GetWorkersByCompanyId(authorizationService.CurrentWorker.CompanyId) ?? new();
        }
    }

    private void AssignJob()
    {
        try
        {
            // Find the selected worker.
            SelectedWorker = Workers.FirstOrDefault(w => w.Id == SelectedWorkerId);
            if (SelectedWorker == null)
            {
                throw new Exception("No worker selected.");
            }

            if (!CreateNewRole)
            {
                // Assign an existing job role.
                if (string.IsNullOrEmpty(SelectedJobRoleId))
                {
                    throw new Exception("No job role selected.");
                }

                workerService.SaveJobRoleToWorker(SelectedWorker, SelectedJobRoleId);
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

                workerService.AddNewJobRole(jobRole);
                workerService.SaveJobRoleToWorker(SelectedWorker, jobRole.Id);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error Saving Job Role - {ex.Message}";
            ShowErrorMessage = true;
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
