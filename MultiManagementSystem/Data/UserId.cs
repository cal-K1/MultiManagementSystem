using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.Data
{
    public class UserId
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public int MonthlySalary { get; set; } = 0;
        public int WeeklyHours { get; set; } = 0;
        public int LeaveDaysRemaining { get; set; } = 0;
    }
}
 