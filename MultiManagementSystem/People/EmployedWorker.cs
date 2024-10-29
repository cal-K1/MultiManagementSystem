using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class EmployedWorker : Worker
{
    [Required]
    public string Id { get; set; } = string.Empty;
    public EmployeeType EmployeeType { get; set; }
}


public enum EmployeeType
{
    PartTime = 0,
    FullTime = 1,
    None = 2,
}
