using Microsoft.AspNetCore.Components;

namespace MultiManagementSystem.Components.Pages;

public partial class Home
{
    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

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
}
