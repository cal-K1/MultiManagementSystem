using Microsoft.AspNetCore.Components;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class CompanyInfoPage
{
    [Inject]
    private ICompanyService CompanyService { get; set; } = default!;
    public string CompanyName { get; set; } = string.Empty;
    public string AdminUsername { get; set; } = string.Empty;
    public int NumberOfEmployees { get; set; } = 0;
    private Admin CurrentAdmin { get; set; } = new Admin();

    public string NumberOfEmployeesString()
    {
        return $"{CompanyName} has {NumberOfEmployees} employees.";
    }

    //public void SetCompanyInfo()
    //{
    //    CurrentAdmin = 

    //    CompanyName = CompanyService.GetCurrentCompany(CurrentAdmin.Id).CompanyName;
    //}
}
