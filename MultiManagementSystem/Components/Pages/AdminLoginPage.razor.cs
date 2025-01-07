using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class AdminLoginPage
{
    [Inject]
    private LogFactory LogFactory { get; set; } = default!;
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
        if (AuthorizationService.IsAdminLoginSuccessful(AdminPassword, AdminUsername))
        {
            NavigationManager.NavigateTo("/home");

            var logger = LogFactory.CreateLogger("ComponentNavigation", LoggerType.ConsoleLogger);
            logger.Info("Navigated to home");
        }
        else
        {
            // Simulate a login process
            ResetForm();
            Message = "Login attempt failed \n Please try again";

            var logger = LogFactory.CreateLogger("InvalidLogin", LoggerType.ConsoleLogger);
            logger.Info("Login Invalid, form reset");
        }
    }

    private void ResetForm()
    {
        AdminUsername = string.Empty;
        AdminPassword = string.Empty;
        Message = string.Empty;
    }
}
