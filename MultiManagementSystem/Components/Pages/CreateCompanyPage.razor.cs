using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateCompanyPage
{
    [Inject]
    private IAuthorizationService AuthorizationService { get; set; } = default!;
    [Inject]
    private ICompanyService CompanyService { get; set; } = default!;
    ILog Logger { get; set; } = default!;
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;
    public string CompanyName { get; set; } = string.Empty;
    public string AdminName { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public Company Company { get; set; } = new Company();
    public bool ShowErrorMessage { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;

    public void HandleValidSubmit()
    {
        if (AuthorizationService.IsPasswordValid(AdminPassword) && AuthorizationService.IsUserNameValid(AdminName) && AuthorizationService.IsUserNameValid(CompanyName))
        {
            Company = new Company
            {
                CompanyName = CompanyName,
                Id = Guid.NewGuid().ToString(),
                Admin = new Admin()
                {
                    Username = AdminName,
                    Password = AdminPassword,
                    Id = Guid.NewGuid().ToString()
                }
            };

            CompanyService.CreateCompany(Company);
            AuthorizationService.SetCurrentAdmin(Company.Admin);

            NavigationManager.NavigateTo("/home");
            Logger.Info("Navigated to /home");
        }
        else
        {
            ErrorMessage = "Invalid Admin Name, Company Name, or Password";
            ShowErrorMessage = true;
        }
    }
}
