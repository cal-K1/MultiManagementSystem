using MultiManagementSystem.People;

namespace MultiManagementSystem.Components.Pages;

public partial class CreateNewWorkerPage
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public EmployeeType EmployeeType { get; set; } = EmployeeType.None;

    public bool isFullTime = false;
    public bool showForm = false;

    private void SelectEmployeeType(bool fullTime)
    {
        isFullTime = fullTime;
        showForm = true;
    }

    public void SetNewWorker()
    {
        
    }
}
