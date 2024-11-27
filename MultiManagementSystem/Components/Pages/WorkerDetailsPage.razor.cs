﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class WorkerDetailsPage
{
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;
    public Worker CurrentWorker { get; set; } = default!;
    public string Message { get; set; } = string.Empty;

    public void SetWorkerDetails()
    {
        CurrentWorker = authorizationService.CurrentWorker;

        if (CurrentWorker == null)
        {
            Message = "Current worker not found, please try again.";
        }
    }
}