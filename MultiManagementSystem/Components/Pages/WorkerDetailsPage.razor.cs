using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class WorkerDetailsPage
{
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    public Worker CurrentWorker { get; set; } = default!;
    public string Message { get; set; } = string.Empty;
    private string JobDescription {  get; set; } = string.Empty;

    public async void SetWorkerDetails()
    {
        CurrentWorker = authorizationService.CurrentWorker;

        List<JobRole> JobRoles = await databaseService.GetAllJobRolesByCompanyId(CurrentWorker.CompanyId);

        foreach ( JobRole jobRole in JobRoles )
        {
            if (jobRole.Id == CurrentWorker.JobRoleId)
            {
                JobDescription = jobRole.Description;
            }
        }


        if (CurrentWorker == null)
        {
            Message = "Current worker not found, please try again.";
            return;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Run(() => SetWorkerDetails());
    }
}
