using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateAdminPage
{
    [Inject]
    private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject]
    private ICompanyService CompanyService { get; set; } = default!;
    ILog Logger { get; set; } = default!;
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
            await CompanyService.CreateAdmin(AdminUsername, AdminPassword);

            NavigationManager.NavigateTo("/home");
            Logger.Info("Navigated to /home");
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
