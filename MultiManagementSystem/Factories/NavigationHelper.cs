﻿using Microsoft.AspNetCore.Components;

namespace MultiManagementSystem.FactorIES
{
    public class NavigationHelper
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        public void Navigate(string url)
        {
            NavigationManager.NavigateTo(url);
        }
    }
}
