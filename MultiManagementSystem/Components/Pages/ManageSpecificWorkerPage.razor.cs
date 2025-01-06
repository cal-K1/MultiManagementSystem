using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ManageSpecificWorkerPage
{
    [Inject]
    IDatabaseService databaseService { get; set; } = default!;

    [Inject]
    NavigationManager navigationManager { get; set; } = default!;

    [Parameter]
    public string WorkerId { get; set; } = string.Empty;

    private Worker SpecificWorker { get; set; } = default!;
    private bool SpecificWorkerDeleted = false;

    protected override async Task OnInitializedAsync()
    {
        SpecificWorker = await databaseService.GetWorkerById(WorkerId);
    }

    public void RemoveWorker()
    {
        databaseService.RemoveWorker(SpecificWorker);
    }
}

