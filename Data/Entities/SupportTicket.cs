namespace SampleCRMApp.Data.Entities;

public class SupportTicket
{
    public Guid TicketId { get; set; }
    public Guid CustomerId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public Guid? AssignedAgentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }

    public Customer Customer { get; set; } = null!;
    public User? AssignedAgent { get; set; }
    public ICollection<TicketResponse> TicketResponses { get; set; } = new List<TicketResponse>();
}
