﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Factories;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class Home
{
    Worker worker = null!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;

    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;

    public required NavigationHelper NavigationHelper { get; set; }
    private bool isSidebarOpen = false;
    private List<Notification> notifications = null!;
    private string errorMessage = string.Empty;
    private bool showErrorMessage = false;

    private async Task ToggleSidebar()
    {
        isSidebarOpen = !isSidebarOpen;

        await GetNotifications();
    }

    private async Task GetNotifications()
    {
        if (authorizationService.CurrentWorker == null && authorizationService.CurrentAdmin == null)
        {
            errorMessage = "No User logged in";
            showErrorMessage = true;
        }
        else if (authorizationService.CurrentWorker != null)
        {
            notifications = await databaseService.GetWorkerNotifications(authorizationService.CurrentWorker);
        }
    }

    private bool IsWorkerManager()
    {
        if (authorizationService.CurrentWorker == null && authorizationService.CurrentAdmin == null)
        {
            return false;
        }

        if (authorizationService.CurrentWorker == null && authorizationService.CurrentAdmin != null)
        {
            return true;
        }

        if (authorizationService.CurrentWorker.Manager == true)
        {
            return true;
        }

        return false;
    }

    private void ClearNotification(Notification notification)
    {
        if (authorizationService.CurrentWorker == null)
        {
            errorMessage = "No User logged in";
            showErrorMessage = true;
        }
        else
        {
            databaseService.RemoveWorkerNotification(authorizationService.CurrentWorker, notification);
            notifications.Remove(notification);
        }
    }
}