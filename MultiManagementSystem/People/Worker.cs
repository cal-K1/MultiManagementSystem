using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public abstract class Worker : User
{
    public string JobTitle = string.Empty;
     
    public int MonthlySalary = 0;
     
    public int WeeklyHours = 0;
     
    public int LeaveDaysRemaining = 0;
}
