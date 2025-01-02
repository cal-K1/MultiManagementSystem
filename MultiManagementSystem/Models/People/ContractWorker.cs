using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.Models.People;

public class ContractWorker : Worker
{
    [Required]
    public new string Id { get; set; } = string.Empty;

    public DateTime ContractStartDate { get; set; } = default!;

    public DateTime ContractEndDate { get; set; } = default!;
}
