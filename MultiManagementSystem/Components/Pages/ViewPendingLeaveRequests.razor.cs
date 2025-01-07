using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models;
using MultiManagementSystem.Models.People;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewPendingLeaveRequests
{
    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;
    [Inject]
    private ICompanyService companyService { get; set; } = default!;

    [Inject]
    private LogFactory LogFactory { get; set; } = default!;

    private List<LeaveRequest> pendingLeaveRequests { get; set; } = new();
    private bool isLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(companyService?.CurrentCompany?.Id))
            {
                var logger = LogFactory.CreateLogger("CompanyService", LoggerType.ConsoleLogger);
                logger.Error("CompanyService / CompanyService.CurrentCompany was null");
            }

            pendingLeaveRequests = await databaseService.GetAllPendingLeaveRequestsByCompanyId(companyService.CurrentCompany.Id);
        }
        catch (Exception ex)
        {
            var logger = LogFactory.CreateLogger("ComponentNavigation", LoggerType.ConsoleLogger);
            logger.Error($"Message: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task HandleRequestAsync(LeaveRequest request, bool isAccepted)
    {
        if (request == null)
        {
            var logger = LogFactory.CreateLogger("Component", LoggerType.ConsoleLogger);
            logger.Error("request was null");

            return;
        }

        await databaseService.HandleLeaveRequestInDatabase(request, isAccepted);
        pendingLeaveRequests.Remove(request);
    }
}
