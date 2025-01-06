using Microsoft.AspNetCore.Components;
using MultiManagementSystem.Models;
using MultiManagementSystem.Services.Abstraction;

namespace MultiManagementSystem.Components.Pages;

public partial class ViewPendingLeaveRequests
{
    [Inject]
    private IDatabaseService databaseService { get; set; } = default!;
    [Inject]
    private ICompanyService companyService { get; set; } = default!;
    [Inject]
    private IAuthorizationService authorizationService { get; set; } = default!;
    private List<LeaveRequest> pendingLeaveRequests { get; set; } = new();
    private bool isLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(companyService?.CurrentCompany?.Id))
            {
                throw new ArgumentNullException(nameof(companyService.CurrentCompany.Id));
            }

            pendingLeaveRequests = await databaseService.GetAllPendingLeaveRequestsByCompanyId(companyService.CurrentCompany.Id);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading leave requests: {ex.Message}");
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
            throw new ArgumentNullException(nameof(request));
        }

        await databaseService.HandleLeaveRequestInDatabase(request, isAccepted);
        pendingLeaveRequests.Remove(request);
    }
}
