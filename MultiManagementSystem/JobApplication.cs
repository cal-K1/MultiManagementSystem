using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem;

public class JobApplication
{
    public required string ApplicationID = string.Empty;

    public string ApplicantName { get; set; } = string.Empty;

    public string ApplicantPhoneNumber { get; set; } = string.Empty;

    public string ApplicationText { get; set; } = string.Empty;

    public ApplicationState ApplicationState { get; set; } = ApplicationState.Pending;
}
