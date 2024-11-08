using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateNewWorkerPage
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NewWorkerNumber {  get; set; } = string.Empty;
    public string SelectedEmployeeType { get; set; } = string.Empty;
    public bool isEmployed = false;
    public bool showForm = false;
    public bool showConfirmation = false; // New flag for confirmation

    private void SelectEmployeeType(bool isEmployedSelected)
    {
        showForm = true;
        isEmployed = isEmployedSelected;

        if (isEmployed)
        {
            SelectedEmployeeType = string.Empty;
        }
    }

    public void CreateNewWorker()
    {
        Worker worker = new Worker()
        {
            Name = Name,
            Id = Guid.NewGuid().ToString(),
            WorkerNumber = workerService.CreateNewWorkerNumber(),
            Password = Password,
        };

        NewWorkerNumber = worker.WorkerNumber;
        workerService.CreateNewWorker(worker.Name, worker.Password, worker.WorkerNumber);

        // Show the confirmation screen after successful creation
        showForm = false;
        showConfirmation = true;
    }

    private void ResetForm()
    {
        // Reset the form to allow creating another worker
        Name = string.Empty;
        Password = string.Empty;
        SelectedEmployeeType = string.Empty;
        showForm = false;
        showConfirmation = false;
    }

    private void NavigateHome()
    {
        NavigationManager.NavigateTo("/");
    }
}