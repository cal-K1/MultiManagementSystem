using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Factories;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class Home
{
    Worker worker = null!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    public required NavigationHelper NavigationHelper { get; set; }
    private bool isSidebarOpen = false;
    private List<string> notifications = null!;
    private string errorMessage = string.Empty;
    private bool showErrorMessage = false;

    private void ToggleSidebar()
    {
        isSidebarOpen = !isSidebarOpen;
    }

    private void GetNotifications()
    {
        if (authorizationService.CurrentWorker == null && authorizationService.CurrentAdmin == null)
        {
            errorMessage = "No User logged in";
            showErrorMessage = true;
        }
        else if (authorizationService.CurrentWorker != null)
        {
            notifications = authorizationService.CurrentWorker.Notifications;
        }
    }

    private bool IsWorkerManager()
    {
        if (authorizationService.CurrentWorker == null && authorizationService.CurrentAdmin == null)
        {
            return false;
        }

        if (authorizationService.CurrentWorker == null && authorizationService.CurrentAdmin != null)
        {
            return true;
        }

        if (authorizationService.CurrentWorker.Manager == true)
        {
            return true;
        }

        return false;
    }
}