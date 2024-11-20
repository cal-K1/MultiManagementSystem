using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages
{
    public partial class LoginPage
    {
        [Inject]
        private IAuthorizationService authorizationService { get; set; } = default!;
        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;
        private string WorkerNumber { get; set; } = string.Empty;
        private string Password { get; set; } = string.Empty;
        private string Message { get; set; } = string.Empty;

        private async Task Submit()
        {
            // Basic validation for demonstration
            if (await authorizationService.IsLoginSuccessful(Password, WorkerNumber))
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
                Message = "Login attempt failed \n Please try again";
            }
        }

        private void NavigateCreate()
        {
            NavigationManager.NavigateTo("/create");
        }
    }
}
