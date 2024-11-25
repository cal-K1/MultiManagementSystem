using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateCompanyPage
{
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;
    public string CompanyName { get; set; } = string.Empty;
    public string AdminName { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public Company Company { get; set; } = new Company();
    public bool ShowErrorMessage { get; set; } = false;
    public string ErrorMessage { get; set; } = string.Empty;

    public void HandleValidSubmit()
    {
        if (authorizationService.IsPasswordValid(AdminPassword) && authorizationService.IsUserNameValid(AdminName) && authorizationService.IsUserNameValid(CompanyName))
        {
            Company = new Company
            {
                CompanyName = CompanyName,
                Admin = new Admin()
                {
                    Name = AdminName,
                    Password = AdminPassword,
                    Id = Guid.NewGuid().ToString()
                }
            };


        }
        else
        {
            ErrorMessage = "Invalid Admin Name, Company Name, or Password";
            ShowErrorMessage = true;
        }
    }
}
