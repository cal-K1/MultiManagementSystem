﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class RequestLeave
{
    [Inject]
    private ILeaveService leaveService { get; set; } = default!;
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;
    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private LogFactory LogFactory { get; set; } = default!;

    public DateTime RequestLeaveStart { get; set; } = DateTime.Now.Date;
    public DateTime RequestLeaveEnd { get; set;} = DateTime.Now.Date;
    public string RequestDescription { get; set; } = string.Empty;
    private bool IsRequestSubmitted { get; set; } = false;

    private async Task SubmitLeaveRequest()
    {
        try
        {
            LeaveRequest leaveRequest = new()
            {
                Id = Guid.NewGuid().ToString(),
                Worker = authorizationService.CurrentWorker,
                StartDate = RequestLeaveStart,
                EndDate = RequestLeaveEnd,
                LeaveDescription = RequestDescription,
                State = LeaveRequestState.Pending,
            };

            await databaseService.AddNewLeaveRequest(authorizationService.CurrentWorker, leaveRequest);
            IsRequestSubmitted = true;
        }
        catch (Exception ex)
        { }
    }

    public void NavigateHome()
    {
        NavigationManager.NavigateTo("/home");

        var logger = LogFactory.CreateLogger("ComponentNavigation", LoggerType.ConsoleLogger);
        logger.Info($"Navigated to /home");
    }
}
