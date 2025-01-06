using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface ILeaveService
{
    /// <summary>
    /// Acceps the given leave request and applies the paramaters passed in.
    /// </summary>
    void AcceptLeave(DateTime startDate, DateTime endDate, Worker worker = null!);
}
