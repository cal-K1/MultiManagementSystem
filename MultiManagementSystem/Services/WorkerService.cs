using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiManagementSystem.Data;
using MultiManagementSystem.Logger;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;
using System.Security;

namespace MultiManagementSystem.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly ManagementSystemDbContext dbContext;
        private readonly ICompanyService companyService;
        private readonly ILog _log;

        public WorkerService(ManagementSystemDbContext dbContext, ICompanyService companyService, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.companyService = companyService;
            _log = new FileLogger("WorkerService", configuration);
        }

        public int GetWorkerLeaveDaysRemaining(string workerId)
        {
            var worker = dbContext.Workers.FirstOrDefault(w => w.Id == workerId);

            if (worker == null)
            {
                return -1;
            }

            return worker.LeaveDaysRemaining;
        }

        public string CreateNewWorkerNumber()
        {
            string workerNumber = string.Empty;
            bool isWorkerNumberUnique = false;

            var random = new Random();

            while (!isWorkerNumberUnique)
            {
                char randomLetter = (char)random.Next('A', 'Z' + 1);
                int randomNumber = random.Next(0, 1000000);

                workerNumber = $"{randomLetter}{randomNumber:D6}";

                if (!IsWorkerNumberAlreadyInUse(workerNumber))
                {
                    isWorkerNumberUnique = true;
                }
            }

            return workerNumber;
        }

        public async Task CreateNewWorkerInDb(Worker worker)
        {
            if (companyService?.CurrentCompany?.Id == null)
            {
                throw new Exception(message: $"Current company is null.");
            }

            worker.CompanyId = companyService.CurrentCompany.Id;
            await dbContext.Workers.AddAsync(worker);
            await dbContext.SaveChangesAsync();
        }

        private bool IsWorkerNumberAlreadyInUse(string workerNumber)
        {
            if (dbContext.Workers.Any(w => w.WorkerNumber == workerNumber))
            {
                return true;
            }

            return false;
        }

        public List<Worker> GetWorkersByCountry(WorkerCountry country) => dbContext.Workers.Where(worker => worker.Country == country).ToList();

        public void NavigateNotification(string notification)
        {
            return;
        }
    }
}
