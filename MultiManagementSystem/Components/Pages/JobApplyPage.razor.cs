﻿using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class JobApplyPage
{
    [Inject]
    private IApplicationService applicationService { get; set; } = default!;

    private string _name { get; set; } = string.Empty;
    private string _phoneNumber { get; set; } = string.Empty;
    private string _description { get; set; } = string.Empty;

    public async Task SubmitForm()
    {
        await applicationService.ApplyJob(_name, _phoneNumber, _description);
    }
}