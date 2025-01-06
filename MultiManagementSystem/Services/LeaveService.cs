using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Data;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class LeaveService(ManagementSystemDbContext dbContext) : ILeaveService
{
    [Inject]
    public IWorkerService workerService { get; set; } = default!;

    public void AcceptLeave(DateTime startDate, DateTime endDate, Worker worker)
    {
        if (worker == null || worker.Id == null)
        {
            throw new ArgumentNullException(nameof(worker));
        }

        int daysRequested = GetLeaveTimeSpan(startDate, endDate);

        if (workerService.GetWorkerLeaveDaysRemaining(worker.Id) >= daysRequested)
        {
            worker.LeaveDaysRemaining -= daysRequested;
            dbContext.Update(worker);
        }
    }

    public int GetLeaveTimeSpan(DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).Days;
    }
}
