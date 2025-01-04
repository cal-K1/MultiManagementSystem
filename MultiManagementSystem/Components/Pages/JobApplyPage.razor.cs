using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class JobApplyPage
{
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;
    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    private string _name { get; set; } = string.Empty;
    private string _phoneNumber { get; set; } = string.Empty;
    private string _description { get; set; } = string.Empty;
    private string _errorMessage { get; set; } = string.Empty;
    private bool _submitted { get; set; } = false;
    private Worker Applicant { get; set; } = default!;

    protected override void OnInitialized()
    {
        Applicant = authorizationService.CurrentWorker;

        if (Applicant == null)
        {
            NavigationManager.NavigateTo("/error", true);
        }
    }


    public async Task SubmitForm()
    {
        if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_phoneNumber) || string.IsNullOrWhiteSpace(_description))
        {
            _errorMessage = "Please fill in all fields.";
        }
        if (string.IsNullOrWhiteSpace(Applicant.Id))
        {
            _errorMessage = "No user logged in, please log in to continue";
        }

        await databaseService.ApplyJob(Applicant.Id ,_name, _phoneNumber, _description);
        _submitted = true;
    }

    private void ReturnToHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
