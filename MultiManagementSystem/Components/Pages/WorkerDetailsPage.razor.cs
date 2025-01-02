using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class WorkerDetailsPage
{
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    public Worker CurrentWorker { get; set; } = default!;
    public string Message { get; set; } = string.Empty;

    public void SetWorkerDetails()
    {
        CurrentWorker = authorizationService.CurrentWorker;

        if (CurrentWorker == null)
        {
            Message = "Current worker not found, please try again.";
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => SetWorkerDetails());
    }
}
