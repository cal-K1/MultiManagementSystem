using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class User
{
    string Name { get; set; } = string.Empty;

    string Password = string.Empty;
}
