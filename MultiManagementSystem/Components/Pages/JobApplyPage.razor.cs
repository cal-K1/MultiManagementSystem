using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models;
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
    private ICompanyService companyService { get; set; } = default!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;
    [Inject]
    private LogFactory LogFactory { get; set; } = default!;

    private string _name { get; set; } = string.Empty;
    private string _phoneNumber { get; set; } = string.Empty;
    private string _description { get; set; } = string.Empty;
    private string _errorMessage { get; set; } = string.Empty;
    private bool _submitted { get; set; } = false;
    private bool _showErrorMessage { get; set; } = false;
    private string _selectedJobRole { get; set; } = string.Empty;
    private List<JobRole> _jobRoles { get; set; } = new List<JobRole>();
    private Worker Applicant { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Applicant = authorizationService.CurrentWorker;

        if (Applicant == null)
        {
            _errorMessage = "Applicant is null, try again";

            var logger = LogFactory.CreateLogger("CompanyService", LoggerType.ConsoleLogger);
            logger.Error("CompanyService / CompanyService.CurrentCompany is null");
            return;
        }

        _jobRoles = await databaseService.GetAllJobRolesByCompanyId(companyService.CurrentCompany.Id);
    }

    public async Task SubmitForm()
    {
        if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_phoneNumber) || string.IsNullOrWhiteSpace(_description) || string.IsNullOrWhiteSpace(_selectedJobRole))
        {
            _errorMessage = "Please fill in all fields.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Applicant.Id))
        {
            _errorMessage = "No user logged in, please log in to continue";
            return;
        }

        var jobApplication = new JobApplication
        {
            ApplicantId = Applicant.Id,
            ApplicantName = _name,
            ApplicantPhoneNumber = _phoneNumber,
            ApplicationText = _description,
            JobRole = _jobRoles.FirstOrDefault(r => r.Id == _selectedJobRole) ?? null!,
            ApplicationState = ApplicationState.Pending
        };

        await databaseService.ApplyJob(jobApplication.ApplicantId, jobApplication.ApplicantName, jobApplication.ApplicantName, jobApplication.ApplicationText);
        _submitted = true;
    }
}
