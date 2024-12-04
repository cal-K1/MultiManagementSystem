using MultiManagementSystem.Data;
using MultiManagementSystem.Services.Abstraction;
using System.Runtime.CompilerServices;

namespace MultiManagementSystem.Services;

public class JobRoleService(ManagementSystemDbContext dbContext) : IJobRoleService
{
    public async Task AddNewJobRole(string title, string description, int salary, string Id)
    {
        //await dbContext.JobRole.AddAsync(new JobRole
        //{
        //    JobTitle = title,
        //    Description = description,
        //    Salary = salary
        //});
    }
}
