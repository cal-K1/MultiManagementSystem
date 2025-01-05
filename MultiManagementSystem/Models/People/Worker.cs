using System.ComponentModel.DataAnnotations.Schema;

namespace MultiManagementSystem.Models.People;

public class Worker : UserBase
{
    [ForeignKey("UserId")]
    public string? Id { get; set; }
    public string WorkerNumber { get; set; } = string.Empty;
    public bool Manager { get; set; } = false;
    public override string Password { get; set; } = string.Empty;
    public WorkerCountry Country { get; set; } = WorkerCountry.Default;
    public string JobRoleId { get; set; } = string.Empty;
    public string CompanyId { get; set; } = string.Empty;

    private List<string> _notifications = new();
    public List<string> Notifications
    {
        get => _notifications;
        set => _notifications = value ?? new List<string>();
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
