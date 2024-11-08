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
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;
    LeaveRequest LeaveRequest { get; set; } = default!;
    public DateTime RequestLeaveStart { get; set; } = DateTime.Now.Date;
    public DateTime RequestLeaveEnd { get; set;} = DateTime.Now.Date;
    public string RequestDescription { get; set; } = string.Empty;
    private bool IsRequestSubmitted { get; set; } = false;

    public string? Id { get; set; }// = //CurrentUser.Id;

    private async Task SubmitLeaveRequest()
    {
        try
        {
            Worker worker = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Callum",
            };

            LeaveRequest leaveRequest = new()
            {
                Id = Guid.NewGuid().ToString(),
                Worker = worker,
                WorkerName = worker.Name,
                WorkerId = worker.Id,
                StartDate = RequestLeaveStart,
                EndDate = RequestLeaveEnd,
                LeaveDescription = RequestDescription,
                State = LeaveRequestState.Pending,
            };

            await leaveService.AddNewLeaveRequest(worker, leaveRequest);
            IsRequestSubmitted = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex}");
        }
    }

    public void NavigateHome()
    {
        NavigationManager.NavigateTo("/home");
    }
}
