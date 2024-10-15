using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class LeaveService
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    private string x = string.Empty;

    private void AcceptLeave(string WorkerId, DateTime startDate, DateTime endDate, ContractWorker contractWorker = null!, EmployedWorker employedWorker = null!)
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

    private void RequestLeave(string WorkerId, DateTime startDate, DateTime endDate, ContractWorker contractWorker = null!, EmployedWorker employedWorker = null!)
    {
        if (contractWorker == null && employedWorker == null)
        {
            return;
        }
    }
}
