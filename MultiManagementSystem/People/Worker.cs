using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public abstract class Worker : User
{
    public string WorkerNumber { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public int MonthlySalary { get; set; } = 0;
    public int WeeklyHours { get; set; } = 0;
    public int LeaveDaysRemaining { get; set; } = 0;
}
