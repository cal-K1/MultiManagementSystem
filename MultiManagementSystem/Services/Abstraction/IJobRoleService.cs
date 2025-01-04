namespace MultiManagementSystem.Services.Abstraction;

public interface IJobRoleService
{
    /// <summary>
    /// Creates a new job role and saves it in the database.
    /// </summary>
    /// <returns></returns>
    public Task AddNewJobRole(string title, string description, int salary, string Id);
}
