using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

/// <summary>
/// Provides methods for managing company-related operations.
/// </summary>
public interface ICompanyService
{
    public Company? CurrentCompany { get; }

    /// <summary>
    /// Creates a new company and saves it to the database.
    /// </summary>
    Task CreateCompany(Company newCompany);

    /// <summary>
    /// Sets the current company based on the worker's ID.
    /// </summary>
    void SetCurrentCompany(string workerId);

    /// <summary>
    /// Sets the current company based on the provided administrator.
    /// </summary>
    void SetCurrentCompanyAsAdmin(Admin admin);

    /// <summary>
    /// Creates a new administrator for the currently selected company.
    /// </summary>
    Task CreateAdmin(string username, string password);

    /// <summary>
    /// Retrieves the company information for the specified company ID.
    /// </summary>
    /// <returns>The company associated with the given ID.</returns>
    Company GetCurrentCompany(string companyId);

    /// <summary>
    /// Retrieves all job roles for a specified company.
    /// </summary>
    /// <returns>A list of job roles associated with the company.</returns>
    Task<List<JobRole>> GetAllJobRolesByCompanyId(string companyId);
}
