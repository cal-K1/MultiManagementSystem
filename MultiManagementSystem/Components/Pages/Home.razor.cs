using Microsoft.AspNetCore.Components;
using MultiManagementSystem.FactorIES;
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
