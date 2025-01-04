using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    /// <summary>
    /// Gets the number of leave days remaining for the worker with the given worker number.
    /// </summary>
    /// <returns>The number of leave days remaining for a given worker.</returns>
    int GetWorkerLeaveDaysRemaining(string WorkerId);

    /// <summary>
    /// Creates a unique worker number.
    /// </summary>
    /// <returns>A new worker number as a string.</returns>
    public string CreateNewWorkerNumber();

    /// <summary>
    /// Gets all workers from a given country.
    /// </summary>
    /// <returns>A list of Workers that have a Country property that matches the inputted country.</returns>
    List<Worker> GetWorkersByCountry(WorkerCountry country);
}
