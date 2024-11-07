using System.ComponentModel.DataAnnotations.Schema;

namespace MultiManagementSystem.People;

public class Worker : UserBase
{
    [ForeignKey("UserId")]
    public string? Id { get; set; }
    public string WorkerNumber { get; set; } = string.Empty;

    // Implement the Password property from UserBase
    public override string Password { get; set; } = string.Empty;
}
