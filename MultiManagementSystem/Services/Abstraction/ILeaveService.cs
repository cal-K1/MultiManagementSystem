using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface ILeaveService
{
    void AcceptLeave(string WorkerId, DateTime startDate, DateTime endDate, Worker worker = null!);
    Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest);
}
