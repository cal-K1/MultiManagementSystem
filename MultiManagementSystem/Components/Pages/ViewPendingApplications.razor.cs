using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewPendingApplications
{
    [Inject]
    private LogFactory LogFactory { get; set; } = default!;

    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;

    private readonly NavigationManager _navigationManager = default!;

    public List<JobApplication> JobApplications { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        // Load pending job applications from the service.
        JobApplications = await databaseService.GetAllPendingJobApplications();
    }

    private void NavigateToApplication(string Id)
    {
        _navigationManager.NavigateTo("/apply");

        var logger = LogFactory.CreateLogger("ComponentNavigation", LoggerType.ConsoleLogger);
        logger.Info("Navigated to /apply");
    }
}
