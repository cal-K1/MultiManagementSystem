namespace MultiManagementSystem.Services.Abstraction;

public interface ICompanyService
{
    public Company? CurrentCompany { get; }
    Task CreateCompany(Company newCompany);
    void SetCurrentCompany(string workerId);
    Company GetCurrentCompany(string companyId);
}
