using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class LeaveService(ManagementSystemDbContext dbContext) : ILeaveService
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    private string x = string.Empty;

    public void AcceptLeave(string WorkerId, DateTime startDate, DateTime endDate, ContractWorker contractWorker = null!, EmployedWorker employedWorker = null!)
    {
        if (contractWorker == null && employedWorker == null)
        {
            return;
        }

        TimeSpan leaveTimeSpanRequested = endDate - startDate;
        int daysRequested = leaveTimeSpanRequested.Days;

        if (workerService.GetWorkerLeaveDaysRemaining(WorkerId) >= daysRequested && employedWorker != null)
        {
            employedWorker.LeaveDaysRemaining -= daysRequested;
        }
        else
        {
            contractWorker.LeaveDaysRemaining -= daysRequested;
        }
    }

    public async Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest)
    {
        await dbContext.LeaveRequests.AddAsync(leaveRequest);
        await dbContext.SaveChangesAsync();
    }
}
