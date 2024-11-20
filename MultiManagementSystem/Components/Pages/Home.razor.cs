﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class Home
{
    Worker worker = null!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    private void NavigateApply()
    {
        NavigationManager.NavigateTo("/apply");
    }

    private void NavigateView()
    {
        NavigationManager.NavigateTo("/view");
    }

    private void NavigateCreate()
    {
        NavigationManager.NavigateTo("/create");
    }

    private void NavigateRequest()
    {
        NavigationManager.NavigateTo("/request");
    }

    private void NavigateLogin()
    {
        NavigationManager.NavigateTo("/login");
    }

    private bool IsWorkerManager()
    {
        if (authorizationService.CurrentWorker.Manager == true)
        {
            return true;
        }

        return false;
    }
}
