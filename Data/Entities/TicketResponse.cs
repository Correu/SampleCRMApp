namespace SampleCRMApp.Data.Entities;

public class TicketResponse
{
    public Guid ResponseId { get; set; }
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public SupportTicket Ticket { get; set; } = null!;
    public User User { get; set; } = null!;
}
