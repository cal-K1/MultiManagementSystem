using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateNewWorkerPage
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    [Inject]
    private ICompanyService companyService { get; set; } = default!;

    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NewWorkerNumber { get; set; } = string.Empty;
    public List<JobRole> AllJobRoles { get; set; } = new();
    private string _errorMessage { get; set; } = string.Empty;
    private bool _showErrorMessage = false;
    public bool IsManager { get; set; } = false;
    public string SelectedJobRoleId { get; set; } = string.Empty; // Store the selected JobRole Id
    public WorkerCountry WorkerCountry { get; set; } = WorkerCountry.Default;

    public bool showConfirmation = false;
    public bool showInvalidPassword = false;

    protected override async Task OnInitializedAsync()
    {
        await SetJobRolesList();
    }

    private async Task SetJobRolesList()
    {
        if (authorizationService.CurrentAdmin != null)
        {
            try
            {
                companyService.SetCurrentCompanyAsAdmin(authorizationService.CurrentAdmin);

                AllJobRoles = await databaseService.GetAllJobRolesByCompanyId(companyService.CurrentCompany.Id);
            }
            catch (Exception ex)
            {
                // For debugging in the future.
                Console.WriteLine(ex);
            }
        }
    }

    public void CreateNewWorker()
    {
        var selectedJobRole = AllJobRoles.FirstOrDefault(role => role.Id == SelectedJobRoleId);

        if (selectedJobRole == null)
        {
            // Handle the case where no valid job role is selected.
            _errorMessage = "No job role selected";
            _showErrorMessage = true;
            return;
        }

        Worker worker = new Worker()
        {
            Name = Name,
            Id = Guid.NewGuid().ToString(),
            WorkerNumber = workerService.CreateNewWorkerNumber(),
            JobRoleId = selectedJobRole.Id,
            Password = Password,
            Manager = IsManager,
            CompanyId = companyService.CurrentCompany.Id ?? string.Empty,
        };

        if (!authorizationService.IsPasswordValid(worker.Password))
        {
            showInvalidPassword = true;
            return;
        }

        NewWorkerNumber = worker.WorkerNumber;
        databaseService.CreateNewWorkerInDb(worker);

        // Show the confirmation screen after successful creation
        showConfirmation = true;
    }

    private void ResetForm()
    {
        // Reset the form to allow creating another worker
        Name = string.Empty;
        Password = string.Empty;
        showConfirmation = false;
        IsManager = false;
        SelectedJobRoleId = string.Empty; // Reset the job role selection
    }

    private void NavigateHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
