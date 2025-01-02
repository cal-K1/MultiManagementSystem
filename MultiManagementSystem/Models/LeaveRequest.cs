using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Models;

public class LeaveRequest
{
    public string Id { get; set; } = string.Empty;
    public Worker Worker { get; set; } = default!;
    public string WorkerName { get; set; } = string.Empty;
    public string WorkerId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public string LeaveDescription = string.Empty;
    public LeaveRequestState State { get; set; } = LeaveRequestState.Pending;
}

public enum LeaveRequestState
{
    Pending = 0,
    Accepted = 1,
    Declined = 2,
};
