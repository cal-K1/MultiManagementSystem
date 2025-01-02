using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class AdminLoginPage
{
    [Inject]
    private IAuthorizationService AuthorizationService { get; set; } = default!;
    ILog Logger;
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
            Logger.Info("Admin logged in");
        }
        else
        {
            // Simulate a login process
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
