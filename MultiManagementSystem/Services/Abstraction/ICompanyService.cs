using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface ICompanyService
{
    public Company? CurrentCompany { get; }
    Task CreateCompany(Company newCompany);
    void SetCurrentCompany(string workerId);
    void SetCurrentCompanyAsAdmin(Admin admin);
    Company GetCurrentCompany(string companyId);
}
