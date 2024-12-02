using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CompanyInfoPage
{
    [Inject]
    private ICompanyService CompanyService { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    public string CompanyName { get; set; } = string.Empty;
    public string AdminUsername { get; set; } = string.Empty;
    public int NumberOfEmployees { get; set; } = 0;
    private Admin CurrentAdmin { get; set; } = new Admin();

    public string NumberOfEmployeesString()
    {
        return $"{CompanyName} has {NumberOfEmployees} employees.";
    }

    public void SetCompanyInfo()
    {
        if (CompanyService.CurrentCompany != null)
        {
            CurrentAdmin = CompanyService.CurrentCompany.Admin;

            CompanyName = CompanyService.GetCurrentCompany(CurrentAdmin.Id).CompanyName;
        }
        else
        {
            throw new Exception("Current Company is not set.");
        }
    }

    public void NavigateHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
