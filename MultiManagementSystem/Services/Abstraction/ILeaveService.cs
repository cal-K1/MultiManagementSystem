using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface ILeaveService
{
    /// <summary>
    /// Subtracts the correct number of leave days from the worker.
    /// </summary>
    /// <exception cref="Exception"></exception>
    void AcceptLeave(string WorkerId, DateTime startDate, DateTime endDate, Worker worker = null!);

    /// <summary>
    /// Creates a new leave request and saves it in the database.
    /// </summary>
    Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest);
}
