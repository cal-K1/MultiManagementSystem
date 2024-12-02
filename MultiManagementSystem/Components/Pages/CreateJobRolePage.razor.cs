namespace MultiManagementSystem.Components.Pages;

public partial class CreateJobRolePage
{
    public JobRole NewJobRole { get; set; } = default!;
    public string JobTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Salary { get; set; } = 0;

    public void SetValues()
    {

    }
}
