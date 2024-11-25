using MultiManagementSystem.People;
using System.Runtime.CompilerServices;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateCompanyPage
{
    public string CompanyName { get; set; } = string.Empty;
    public string AdminName { get; set; } = string.Empty;
    public string AdminPassword { get; set; } = string.Empty;
    public Company Company { get; set; } = new Company();

    public void HandleValidSubmit()
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
}
