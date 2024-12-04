using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateJobRolePage
{
    [Inject]
    private IWorkerService WorkerService { get; set; } = default!;
    NavigationManager NavigationManager { get; set; } = default!;
    public JobRole NewJobRole { get; set; } = default!;
    public string JobTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Salary { get; set; } = 0;
    public string Message { get; set; } = string.Empty;
    public bool ShowMessage { get; set; } = false;

    public void SetValues()
    {
        NewJobRole.Id = Guid.NewGuid().ToString();
        NewJobRole.Salary = Salary;
        NewJobRole.JobTitle = JobTitle;
        NewJobRole.Description = Description;
    }

    public void CreateJobRole()
    {
        WorkerService.AddNewJobRole(NewJobRole);
    }

    public void NavigateHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
