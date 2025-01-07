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

    private string _name = string.Empty;
    private string _phoneNumber = string.Empty;
    private string _description = string.Empty;
    private string _selectedJobRole = string.Empty;
    private bool _submitted = false;
    private bool _showErrorMessage = false;
    private string _errorMessage = string.Empty;

    private List<JobRole> _jobRoles = new List<JobRole>
    {
        new JobRole { Id = "FullTime", JobTitle = "Full-Time" },
        new JobRole { Id = "Contract", JobTitle = "Contract" }
    };

    private IJobApplicationStrategy _jobApplicationStrategy;

    protected override async Task OnInitializedAsync()
    {
        // Select the appropriate strategy based on the selected job role.
        if (_selectedJobRole == "FullTime")
        {
            _jobApplicationStrategy = new FullTimeJobStrategy();
        }
        else if (_selectedJobRole == "Contract")
        {
            _jobApplicationStrategy = new ContractJobStrategy();
        }
    }

    public async Task SubmitForm()
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(_name) || string.IsNullOrWhiteSpace(_phoneNumber) || string.IsNullOrWhiteSpace(_description) || string.IsNullOrWhiteSpace(_selectedJobRole))
        {
            _errorMessage = "Please fill in all fields.";
            _showErrorMessage = true;
            return;
        }

        var jobApplication = new JobApplication
        {
            ApplicantId = Guid.NewGuid().ToString(),  
            ApplicantName = _name,
            ApplicantPhoneNumber = _phoneNumber,
            ApplicationText = _description,
            JobRole = _jobRoles.FirstOrDefault(r => r.Id == _selectedJobRole) ?? null!,
            ApplicationState = ApplicationState.Pending
        };

        // Use the strategy to validate the application
        _errorMessage = await _jobApplicationStrategy.ValidateApplication(jobApplication);

        if (_errorMessage != "Application valid for full-time job." && _errorMessage != "Application valid for contract job.")
        {
            return;
        }

        await databaseService.ApplyJob(jobApplication.ApplicantId, jobApplication.ApplicantName, jobApplication.ApplicantPhoneNumber, jobApplication.ApplicationText);
        _submitted = true;
    }
}
