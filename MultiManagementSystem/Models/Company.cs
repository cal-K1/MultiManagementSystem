﻿using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Models
{
    public class Company
    {
        public string Id { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public Admin Admin { get; set; } = new Admin();
    }
}
