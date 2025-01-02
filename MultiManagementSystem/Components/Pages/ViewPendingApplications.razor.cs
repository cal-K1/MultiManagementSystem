using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewPendingApplications
{
    [Inject]
    private IApplicationService applicationService { get; set; } = default!;
    ILog Logger { get; set; } = default!;

    private readonly NavigationManager _navigationManager = default!;

    public List<JobApplication> JobApplications { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // Load pending job applications from the service.
        JobApplications = await applicationService.GetAllPendingJobApplications();
    }

    private void NavigateToApplication(string Id)
    {
        _navigationManager.NavigateTo("/apply");
        Logger.Info("Navigated to /apply");
    }
}
