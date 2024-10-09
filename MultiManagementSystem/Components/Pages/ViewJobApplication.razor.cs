using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewJobApplication
{
    [Inject]
    private IApplicationService applicationService { get; set; } = default!;

    public JobApplication SelectedApplication { get; set; } = new JobApplication();

    public string DisplayApplicantName {  get; set; } = string.Empty;
    public string DisplayApplicantPhoneNumber {  get; set; } = string.Empty;
    public string DisplayApplicantText { get; set; } = string.Empty;


    public void SetJobApplicationValues()
    {
        SelectedApplication = applicationService.GetApplication(SelectedApplication.Id);

        DisplayApplicantName = SelectedApplication.ApplicantName;
        DisplayApplicantPhoneNumber = SelectedApplication.ApplicantPhoneNumber;
        DisplayApplicantText = SelectedApplication.ApplicationText;
    }
}
