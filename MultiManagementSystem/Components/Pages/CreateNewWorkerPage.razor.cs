using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateNewWorkerPage
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
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

    private void SubmitValues()
    {
        if (isEmployed)
        {
            CreateNewEmployedWorker();
        }
        else
        {
            CreateNewContractWorker();
        }

        // Show the confirmation screen after successful creation
        showForm = false;
        showConfirmation = true;
    }

    public void CreateNewEmployedWorker()
    {
        if (SelectedEmployeeType == "FullTime")
        {
            workerService.CreateNewEmployedWorker(Name, Password, EmployeeType.FullTime);
        }
        else
        {
            workerService.CreateNewEmployedWorker(Name, Password, EmployeeType.PartTime);
        }
    }

    public void CreateNewContractWorker()
    {
        workerService.CreateNewContractWorker(Name, Password);
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
}