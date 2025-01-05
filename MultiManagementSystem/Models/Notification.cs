using MultiManagementSystem.Models.People;

namespace MultiManagementSystem.Models;

public class Notification
{
    public string Id { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public Worker NotificationWorker { get; set; } = default!;
    public NotificationType NotificationType { get; set; } = NotificationType.None;
}

public enum NotificationType
{
    None,
    JobRole,
}
