using MultiManagementSystem.People;

namespace MultiManagementSystem.Services.Abstraction;

public interface IWorkerService
{
    /// <summary>
    /// Gets the worker from the db with the given workerNumber.
    /// </summary>
    /// <param name="workerId"></param>
    /// <returns>The worker with given worker number.</returns>
    Task<Worker> GetWorkerByWorkerNumber(string workerNumber);

    /// <summary>
    /// Gets the number of leave days remaining for the worker with the given worker number.
    /// </summary>
    /// <returns>The number of leave days remaining for a given worker.</returns>
    /// <exception cref="InvalidOperationException"></exception
    int GetWorkerLeaveDaysRemaining(string WorkerId);

    /// <summary>
    /// Creates a unique worker number.
    /// </summary>
    /// <returns>A new worker number as a string.</returns>
    public string CreateNewWorkerNumber();

    /// <summary>
    /// Creates a worker with the specified properties passed in as parameters and saves it in the database.
    /// </summary>
    Task CreateNewWorkerInDb(Worker worker);

    /// <summary>
    /// Gets all workers from a given country.
    /// </summary>
    /// <returns>A list of Workers that have a Country property that matches the inputted country.</returns>
    List<Worker> GetWorkersByCountry(WorkerCountry country);

    /// <summary>
    /// Sets the workers JobRole property to the given jobRole and saves the changes to the database.
    /// </summary>
    Task SaveJobRoleToWorker(Worker worker, string jobRoleId);

    /// <summary>
    /// Gets a list of all workers in the database with the given company id.
    /// </summary>
    Task<List<Worker>> GetWorkersByCompanyId(string companyId);

    /// <summary>
    /// Adds a new JobRole in the database and saves it.
    /// </summary>
    Task AddNewJobRole(JobRole jobRole);
}
