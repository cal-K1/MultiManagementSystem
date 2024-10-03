using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class User
{
    public string Name { get; set; } = string.Empty;

    public string Password = string.Empty;
}
