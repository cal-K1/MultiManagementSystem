using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class Management : User
{
    [Required]
    public string Id { get; set; } = string.Empty;

    public bool HasManagerPermissions = true;
}
