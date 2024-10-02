using MultiManagementSystem.Services.Abstraction;
using static System.Net.Mime.MediaTypeNames;

namespace MultiManagementSystem.Services
{
    public class ApplicationService : IApplicationService
    {
        public void GetApplication()
        {
            return;
        }

        public void AcceptApplication(Application application)
        {
            application.ApplicationState = ApplicationState.Accepted;
            return;
        }

        public void DeclineApplication(Application application)
        {
            application.ApplicationState = ApplicationState.Declined;
            return;
        }

        public async Task ApplyJob(string name, string phoneNumber, string applicationText)
        {
            Application newApplication = new Application
            {
                ApplicationID = Guid.NewGuid().ToString(),
                ApplicantName = name,
                ApplicantPhoneNumber = phoneNumber,
                ApplicationText = applicationText
            };
        }
    }

}
