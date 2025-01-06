using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ManageStaffPage
{
    [Inject]
    IDatabaseService databaseService { get; set; } = default!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    ICompanyService companyService { get; set; } = default!;

    public List<Worker> workersList { get; set; } = new();
    public WorkerCountry? SelectedCountry { get; set; }

    protected override async Task OnInitializedAsync()
    {
        workersList = await GetWorkers();
    }

    public async Task<List<Worker>> GetWorkers()
    {
        if (companyService.CurrentCompany == null)
        {
            throw new ArgumentNullException(nameof(companyService));
        }

        workersList = await databaseService.GetWorkersByCompanyId(companyService.CurrentCompany.Id);

        return workersList;
    }

    public async Task<List<Worker>> GetWorkersByCountry(WorkerCountry country)
    {
        if (companyService.CurrentCompany == null)
        {
            throw new ArgumentNullException(nameof(companyService));
        }

        workersList = await databaseService.GetWorkersByCountry(companyService.CurrentCompany.Id, country);

        return workersList;
    }

    public async Task HandleCountryChange(ChangeEventArgs e)
    {
        var countryValue = e.Value?.ToString();

        if (string.IsNullOrWhiteSpace(countryValue))
        {
            // Reset to all workers if no country is selected
            workersList = await GetWorkers();
        }
        else if (Enum.TryParse<WorkerCountry>(countryValue, out var country))
        {
            SelectedCountry = country;
            workersList = await GetWorkersByCountry(country);
        }
    }

    public void NavigateToSpecificWorker(string workerId)
    {
        NavigationManager.NavigateTo($"/manage-worker/{workerId}");
    }
}
