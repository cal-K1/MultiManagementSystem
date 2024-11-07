using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public abstract class UserBase
{
    public string Name { get; set; } = string.Empty;

    public abstract string Password { get; set; }

}
