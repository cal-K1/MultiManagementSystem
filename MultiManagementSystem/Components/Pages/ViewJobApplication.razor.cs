using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewJobApplication
{
    [Inject]
    private IApplicationService applicationService { get; set; } = default!;

    // Accept the ID of the application as a parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;

    public JobApplication SelectedApplication { get; set; } = new JobApplication();

    public string DisplayApplicantName { get; set; } = string.Empty;
    public string DisplayApplicantPhoneNumber { get; set; } = string.Empty;
    public string DisplayApplicantText { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        // Fetch the application data based on the passed Id synchronously
        if (!string.IsNullOrEmpty(Id))
        {
            SelectedApplication = applicationService.GetApplication(Id);
            if (SelectedApplication != null)
            {
                DisplayApplicantName = SelectedApplication.ApplicantName;
                DisplayApplicantPhoneNumber = SelectedApplication.ApplicantPhoneNumber;
                DisplayApplicantText = SelectedApplication.ApplicationText;
            }
        }
    }

    private async Task AcceptApplication()
    {
        await applicationService.AcceptApplication(SelectedApplication);
    }

    private async Task DeclineApplication()
    {
        await applicationService.DeclineApplication(SelectedApplication);
    }
}
