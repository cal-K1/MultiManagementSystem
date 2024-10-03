﻿using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class Management : User
{
    [Required]
    string Id { get; set; } = string.Empty;

    bool HasManagerPermissions = true;
}
