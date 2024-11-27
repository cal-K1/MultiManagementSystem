using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
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
    private NavigationManager NavigationManager { get; set; } = default!;

    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NewWorkerNumber { get; set; } = string.Empty;
    public bool IsManager { get; set; } = false;
    public WorkerCountry WorkerCountry { get; set; } = WorkerCountry.Default;

    public bool isEmployed = false;
    public bool showForm = false;
    public bool showConfirmation = false;
    public bool showInvalidPassword = false;

    private void SelectEmployeeType(bool isEmployedSelected)
    {
        showForm = true;
    }

    public void CreateNewWorker()
    {
        Worker worker = new Worker()
        {
            Name = Name,
            Id = Guid.NewGuid().ToString(),
            WorkerNumber = workerService.CreateNewWorkerNumber(),
            Password = Password,
            Manager = IsManager,
        };

        if (!authorizationService.IsPasswordValid(worker.Password))
        {
            showInvalidPassword = true;
            return;
        }

        NewWorkerNumber = worker.WorkerNumber;
        workerService.CreateNewWorkerInDb(worker);

        // Show the confirmation screen after successful creation
        showForm = false;
        showConfirmation = true;
    }

    private void ResetForm()
    {
        // Reset the form to allow creating another worker
        Name = string.Empty;
        Password = string.Empty;
        showForm = false;
        showConfirmation = false;
        IsManager = false;
    }

    private void NavigateHome()
    {
        NavigationManager.NavigateTo("/");
    }

    private void NavigateAssignJobRole()
    {
        NavigationManager.NavigateTo($"/assign-job-role/{NewWorkerNumber}");
    }
}