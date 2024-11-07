using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class Management : UserBase
{
    [Required]
    public string Id { get; set; } = string.Empty;

    public bool HasManagerPermissions = true;

    // Implement the Password property from UserBase
    public override string Password { get; set; } = string.Empty;
}

