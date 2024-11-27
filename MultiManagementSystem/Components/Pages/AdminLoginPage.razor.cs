﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class AdminLoginPage
{
    [Inject]
    private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    public string AdminUsername { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    private void Submit()
    {
        // Basic validation for demonstration
        if (AuthorizationService.IsAdminLoginSuccessful(AdminUsername, AdminPassword))
        {
            NavigationManager.NavigateTo("/home");
        }
        else
        {
            // Simulate a login process
            ResetForm();
            Message = "Login attempt failed \n Please try again";
        }
    }

    private void NavigateAdminLogin()
    {
        NavigationManager.NavigateTo("/companyinfo");
    }

    private void ResetForm()
    {
        AdminUsername = string.Empty;
        AdminPassword = string.Empty;
        Message = string.Empty;
    }
}