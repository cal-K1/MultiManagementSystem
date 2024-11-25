namespace MultiManagementSystem.Services.Abstraction;

public interface ICompanyService
{
    Task CreateCompany(Company newCompany);
    void SetCurrentCompany(string workerId);
    Company GetCurrentCompany(string companyId);
}
