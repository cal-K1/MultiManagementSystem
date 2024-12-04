namespace MultiManagementSystem.Services.Abstraction;

public interface IJobRoleService
{
    public Task AddNewJobRole(string title, string description, int salary, string Id);
}
