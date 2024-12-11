using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
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
    public Worker SelectedWorker { get; set; } = default!; // New propety for selected worker
    public List<Worker> Workers { get; set; } = new(); // List to store workers

    protected override async Task OnInitializedAsync()
    {
        // Load workers when the page initializes
        Workers = await GetListOfReleventWorkers();
    }

    private async void CheckAuthorization()
    {
        Workers = await GetListOfReleventWorkers();

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

    private void AssignJob()
    {
        try
        {
            // Find the selected worker using the SelectedWorkerId
            SelectedWorker = Workers.FirstOrDefault(w => w.Id == SelectedWorkerId);
            if (SelectedWorker == null)
            {
                throw new Exception("No worker selected.");
            }

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
        catch (Exception ex)
        {
            throw new Exception($"Error Saving JobRole - {ex.Message}");
        }

        Submitted = true;
    }

    private async Task<List<Worker>> GetListOfReleventWorkers()
    {
        if (authorizationService.CurrentWorker == null)
        {
            IsManagerAuthorized = false;
            return null!;
        }
        else
        {
            return await workerService.GetWorkersByCompanyId(authorizationService.CurrentWorker.CompanyId) ?? new();
        }
    }
}
