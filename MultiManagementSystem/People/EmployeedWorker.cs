using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class EmployeedWorker : Worker
{
    [Required]
    public string Id { get; set; } = string.Empty;

    public EmployeeType EmployeType { get; set; }
    
}

public enum EmployeeType
{
    PartTime = 0,
    FullTime = 1,
}
