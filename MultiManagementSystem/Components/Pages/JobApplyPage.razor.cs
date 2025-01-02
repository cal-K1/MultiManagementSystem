using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class JobApplyPage
{
    [Inject]
    private IApplicationService applicationService { get; set; } = default!;
    ILog Logger { get; set; } = default!;
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    private string _name { get; set; } = string.Empty;
    private string _phoneNumber { get; set; } = string.Empty;
    private string _description { get; set; } = string.Empty;
    private bool _submitted { get; set; } = false;

    public async Task SubmitForm()
    {
        await applicationService.ApplyJob(_name, _phoneNumber, _description);
        _submitted = true;
    }

    private void ReturnToHome()
    {
        NavigationManager.NavigateTo("/");
        Logger.Info("Navigated to /");
    }
}
