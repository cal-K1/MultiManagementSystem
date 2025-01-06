using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewJobApplication
{
    [Inject]
    private IApplicationService applicationService { get; set; } = default!;

    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    // Accept the ID of the application as a parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;

    public JobApplication SelectedApplication { get; set; } = new JobApplication();

    public Worker Applicant { get; set; } = default!;

    public string DisplayApplicantName { get; set; } = string.Empty;
    public string DisplayApplicantPhoneNumber { get; set; } = string.Empty;
    public string DisplayApplicantText { get; set; } = string.Empty;

    private bool _applicationDealtWith { get; set; } = false;

    protected async override void OnInitialized()
    {
        // Fetch the application data based on the passed Id.
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
        if (workerService == null)
        {
            throw new ArgumentNullException(nameof(workerService));
        }

        Applicant = await databaseService.GetWorkerById(SelectedApplication.ApplicantId);

        Notification newNotification = new Notification()
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = NotificationType.JobApplication,
            NotificationWorker = Applicant,
            Message = "Your Job Application has been accepted."
        };

        await databaseService.AcceptApplication(SelectedApplication);
        databaseService?.SaveNewNotification(Applicant, newNotification);
        _applicationDealtWith = true;
    }

    private async Task DeclineApplication()
    {
        if (workerService == null)
        {
            throw new ArgumentNullException(nameof(workerService));
        }

        Notification newNotification = new Notification()
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = NotificationType.JobApplication,
            NotificationWorker = Applicant,
            Message = "Your Job Application has been declined."
        };

        await databaseService.DeclineApplication(SelectedApplication);
        databaseService?.SaveNewNotification(Applicant, newNotification);
        _applicationDealtWith = true;
    }
}
