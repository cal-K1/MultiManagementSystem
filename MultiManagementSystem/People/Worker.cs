using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class Worker : User
{
    string JobTitle = string.Empty;

    int MonthlySalary = 0;

    int WeeklyHours = 0;

    int LeaveDaysRemaining = 0;
}
