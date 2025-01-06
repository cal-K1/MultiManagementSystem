using System.ComponentModel.DataAnnotations.Schema;

namespace MultiManagementSystem.Models.People;

public class Worker : UserBase
{
    [ForeignKey("UserId")]
    public string? Id { get; set; }
    public string WorkerNumber { get; set; } = string.Empty;
    public bool Manager { get; set; } = false;
    public override string Password { get; set; } = string.Empty;
    public int LeaveDaysRemaining { get; set; } = 100;
    public WorkerCountry Country { get; set; } = WorkerCountry.Default;
    public string JobRoleId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;

    private List<Notification> _notifications = new();
    public List<Notification> Notifications
    {
        get => _notifications;
        set => _notifications = value ?? new List<Notification>();
    }
}

public enum WorkerCountry
{
    Default,
    UnitedKingdom,
    Germany,
    Morocco,
    UnitedStates,
    Canada,
    Brazil,
    Argentina,
    SouthAfrica,
    Nigeria,
    Egypt,
    China,
    Japan,
    India,
    Australia,
    NewZealand,
    France,
    Italy,
    Spain,
    Russia,
    Mexico,
    SouthKorea,
    SaudiArabia,
    Turkey,
}
