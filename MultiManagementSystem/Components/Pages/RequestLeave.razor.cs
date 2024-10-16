using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class RequestLeave(ManagementSystemDbContext dbContext)
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;
    LeaveRequest LeaveRequest { get; set; } = default!;
    public DateTime RequestLeaveStart { get; set; } = DateTime.Now;
    public DateTime RequestLeaveEnd { get; set;} = DateTime.Now;
    public string RequestDescription { get; set; } = string.Empty;

    public EmployedWorker EmployedWorker { get; set; } = null!;
    public ContractWorker ContractWorker { get; set; } = null!;

    string Id { get; set; }// = CurrentUser.Id;

    private async void SubmitLeaveRequest()
    {
        var worker = await workerService.GetWorker(Id);
         
        LeaveRequest leaveRequest = new()
        { 
            Id = new Guid().ToString(),
            Worker = worker,
            WorkerName = worker.Name,
            StartDate = RequestLeaveStart,
            EndDate = RequestLeaveEnd,
            LeaveDescription = RequestDescription,
            State = LeaveRequestState.Pending,
        };

        //dbContext.LeaveRequest.AddAsync(leaveRequest);
    }
}
