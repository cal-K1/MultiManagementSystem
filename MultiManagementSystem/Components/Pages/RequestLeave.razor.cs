using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class RequestLeave
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;
    [Inject]
    private ILeaveService leaveService { get; set; } = default!;
    LeaveRequest LeaveRequest { get; set; } = default!;
    public DateTime RequestLeaveStart { get; set; } = DateTime.Now;
    public DateTime RequestLeaveEnd { get; set;} = DateTime.Now;
    public string RequestDescription { get; set; } = string.Empty;

    public EmployedWorker EmployedWorker { get; set; } = null!;
    public ContractWorker ContractWorker { get; set; } = null!;

    public string? Id { get; set; }// = //CurrentUser.Id;

    private async Task SubmitLeaveRequest()
    {
        //var worker = await workerService.GetWorker(Id);

        EmployedWorker employedWorker = new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Callum",
            LeaveDaysRemaining = 10,
        };

        LeaveRequest leaveRequest = new()
        { 
            Id = new Guid().ToString(),
            Worker = employedWorker,
            WorkerName = employedWorker.Name,
            StartDate = RequestLeaveStart,
            EndDate = RequestLeaveEnd,
            LeaveDescription = RequestDescription,
            State = LeaveRequestState.Pending,
        };

        await leaveService.AddNewLeaveRequest(employedWorker,leaveRequest);
    }
}
