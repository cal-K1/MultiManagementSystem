using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface ICompanyService
{
    public Company? CurrentCompany { get; }

    /// <summary>
    /// Sets the current company from the worker ID.
    /// </summary>
    void SetCurrentCompany(string workerId);

    /// <summary>
    /// Sets the current company as an admin based on the admin.
    /// </summary>
    void SetCurrentCompanyAsAdmin(Admin admin);

    /// <summary>
    /// Sets the current company based on the company.
    /// </summary>
    void SetCurrentCompany(Company company);

    /// <summary>
    /// Gets the current company based on the company ID.
    /// </summary>
    /// <returns>The company associated with the given company ID.</returns>
    Company GetCurrentCompany(string companyId);
}
