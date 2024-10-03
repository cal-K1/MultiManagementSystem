using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class EmployeedWorker : Worker
{
    [Required]
    string Id { get; set; } = string.Empty;

    EmployeeType EmployeType { get; set; }
    
}

public enum EmployeeType
{
    PartTime = 0,
    FullTime = 1,
}
