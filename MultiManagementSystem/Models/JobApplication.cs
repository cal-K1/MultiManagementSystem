using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.Models;

public class JobApplication
{
    [Key]
    [Required]
    public string Id { get; set; } = string.Empty;
    public string ApplicantId { get; set; }
    public string ApplicantName { get; set; }
    public string ApplicantPhoneNumber { get; set; }
    public string ApplicationText { get; set; }
    public JobRole JobRole { get; set; }
    public ApplicationState ApplicationState { get; set; }
    public bool IsFullTimeWorker { get; set; } = true;
}
