using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateAdminPage
{
    [Inject]
    private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject]
    private LogFactory LogFactory { get; set; } = default!;
    [Inject]
    private IDatabaseService DatabaseService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    public string AdminUsername { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    private async Task Submit()
    {
        // Basic validation for demonstration
        if (AuthorizationService.IsUserNameValid(AdminUsername) && AuthorizationService.IsPasswordValid(AdminPassword))
        {
            await DatabaseService.CreateAdmin(AdminUsername, AdminPassword);

            NavigationManager.NavigateTo("/home");

            var logger = LogFactory.CreateLogger("DatabaseService", LoggerType.ConsoleLogger);
            logger.Info("Admin created, navigated to home.");
        }
        else
        {
            ResetForm();
            Message = "Login attempt failed \n Please try again";
        }
    }

    private void ResetForm()
    {
        AdminUsername = string.Empty;
        AdminPassword = string.Empty;
        Message = string.Empty;
    }
}
