using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Factories;
using MultiManagementSystem.People;
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
    private List<string> notifications = new() { "Notification 1", "Notification 2", "Notification 3" }; // Example notifications

    private void ToggleSidebar()
    {
        isSidebarOpen = !isSidebarOpen;
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
