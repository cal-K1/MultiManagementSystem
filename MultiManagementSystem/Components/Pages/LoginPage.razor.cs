using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages
{
    public partial class LoginPage
    {
        [Inject]
        private IAuthorizationService authorizationService { get; set; } = default!;
        [Inject]
        private IDatabaseService databaseService { get; set; } = default!;
        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;
        private string WorkerNumber { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;
        private string Message { get; set; } = string.Empty;

        private async Task Submit()
        {
            // Basic validation for demonstration
            if (await databaseService.SetUserLoggedIn(Password, WorkerNumber))
            {
                if (authorizationService.CurrentWorker == null)
                {
                    Message = "No User Found";
                    return;
                }

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
            NavigationManager.NavigateTo("/login/admin");
        }

        private void ResetForm()
        {
            WorkerNumber = string.Empty;
            Password = string.Empty;
            Message = string.Empty;
        }
    }
}
