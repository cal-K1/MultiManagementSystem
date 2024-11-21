using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages
{
    public partial class AssignJobRole
    {
        [Inject]
        private IAuthorizationService authorizationService { get; set; } = default!;
        [Inject]
        private IWorkerService workerService { get; set; } = default!;
        public string ManagerWorkerNumber { get; set; } = string.Empty;
        public string ManagerPassword { get; set; } = string.Empty;
        public bool IsManagerAuthorized { get; set; } = false;
        public bool Submitted { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public bool ShowErrorMessage { get; set; } = false;
        public string WorkerJobTitle { get; set; } = string.Empty;
        public int WorkerSalary { get; set; } = 0;
        public string WorkerDescription { get; set; } = string.Empty;

        private async void CheckAuthorization()
        {
            if (await authorizationService.IsLoginSuccessful(ManagerWorkerNumber, ManagerPassword) && (await workerService.GetWorkerByWorkerNumber(ManagerWorkerNumber)).Manager)
            {
                IsManagerAuthorized = true;
            }
            else
            {
                ErrorMessage = "Invalid Manager Login";
                ShowErrorMessage = true;
            }
        }

        private void AssignJob()
        {
            try
            {
                JobRole jobRole = new JobRole()
                {
                    Id = Guid.NewGuid().ToString(), 
                    JobTitle = WorkerJobTitle,
                    Salary = WorkerSalary,
                    Description = WorkerDescription
                };

                workerService.SaveJobRole(authorizationService.CurrentWorker, jobRole);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error Saving JobRole - {ex}");
            }

            Submitted = true;
        }
    }
}
