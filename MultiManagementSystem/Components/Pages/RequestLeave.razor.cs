using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services;
using MultiManagementSystem.Services.Abstraction;
using System.Diagnostics.CodeAnalysis;

namespace MultiManagementSystem.Components.Pages;

public partial class RequestLeave
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;
    LeaveRequest LeaveRequest { get; set; } = default!;
    public DateTime RequestLeaveStart { get; set; } = DateTime.Now;
    public DateTime RequestLeaveEnd { get; set;} = DateTime.Now;
    public string RequestDescription { get; set; } = string.Empty;

    public EmployedWorker EmployedWorker { get; set; } = null!;
    public ContractWorker ContractWorker { get; set; } = null!;

    Worker worker { get; set; }

    private void SubmitLeaveRequest()
    {
        SetWorker("123");

        LeaveRequest leaveRequest = new()
        { 
            Id = new Guid().ToString(),
            WorkerId = "123",
            WorkerName = "123",
            StartDate = RequestLeaveStart,
            EndDate = RequestLeaveEnd,
            LeaveDescription = RequestDescription,
        };
    }

    public void SetWorker(string Id)
    {
        if (EmployedWorker == null && ContractWorker == null)
        {
            return;
        }

        if (workerService.GetEmployedWorker(EmployedWorker.Id) != null)
        {
            var worker = EmployedWorker as Worker;
        }
        else
        {
            var worker = workerService.GetContractWorker(EmployedWorker.Id);
        }
    }
}
