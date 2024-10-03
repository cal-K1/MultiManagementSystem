using System.ComponentModel.DataAnnotations;

namespace MultiManagementSystem.People;

public class ContractWorker : Worker
{
    [Required]
    string Id { get; set; } = string.Empty;

    DateTime ContractStartDate { get; set; } = default!;

    DateTime ContractEndDate { get; set; } = default!;
}
