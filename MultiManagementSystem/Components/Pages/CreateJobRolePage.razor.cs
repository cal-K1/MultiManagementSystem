using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateJobRolePage
{
    [Inject]
    private IWorkerService WorkerService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    public string JobTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Salary { get; set; } = null;
    public string Message { get; set; } = string.Empty;
    public bool ShowMessage { get; set; } = false;
    public bool ShowSuccessScreen { get; set; } = false;

    private JobRole _jobRole = new();

    public void CreateJobRole()
    {
        if (string.IsNullOrEmpty(JobTitle) || string.IsNullOrEmpty(Description) || Salary <= 0 || Salary == null)
        {
            Message = "Please fill in all fields";
            ShowMessage = true;
            ShowSuccessScreen = false;
            return;
        }

        _jobRole.JobTitle = JobTitle;
        _jobRole.Description = Description;
        _jobRole.Salary = Salary ?? 0;

        // Assuming WorkerService handles adding the new job role correctly.
        WorkerService.AddNewJobRole(_jobRole);

        // Reset form values and show success screen
        ResetFormFields();
        ShowMessage = false;
        ShowSuccessScreen = true;
    }

    private void ResetFormFields()
    {
        ShowSuccessScreen = ShowMessage = false;

        JobTitle = string.Empty;
        Description = string.Empty;
        Salary = null;
    }

    private void ResetForm()
    {
        ShowSuccessScreen = false;
        ShowMessage = false;
        ResetFormFields();
    }
}
