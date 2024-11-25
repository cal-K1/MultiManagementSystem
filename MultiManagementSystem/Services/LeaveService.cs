﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Data;
using MultiManagementSystem.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Services;

public class LeaveService(ManagementSystemDbContext dbContext) : ILeaveService
{
    [Inject]
    private IWorkerService workerService { get; set; } = default!;

    /// <summary>
    /// Subtracts the correct number of leave days from the worker.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public void AcceptLeave(string WorkerId, DateTime startDate, DateTime endDate, Worker worker)
    {
        if (worker == null)
        {
            return;
        }

        TimeSpan leaveTimeSpanRequested = endDate - startDate;
        int daysRequested = leaveTimeSpanRequested.Days;

        if (workerService.GetWorkerLeaveDaysRemaining(WorkerId) >= daysRequested)
        {
            var user = dbContext.UserId.FirstOrDefault(u => u.Id == worker.Id);

            if (user == null)
            {
                throw new Exception($"User with Id {worker.Id} not found.");
            }

            user.LeaveDaysRemaining -= daysRequested;
        }
    }

    /// <summary>
    /// Creates a new leave request and saves it in the database.
    /// </summary>
    public async Task AddNewLeaveRequest(Worker worker, LeaveRequest leaveRequest)
    {
        await dbContext.LeaveRequests.AddAsync(leaveRequest);
        await dbContext.SaveChangesAsync();
    }
}
