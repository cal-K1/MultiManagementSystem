using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class User
{
    [Key]
    public required string UserID = string.Empty;

    string Name { get; set; } = string.Empty;

    string Password = string.Empty;
}
