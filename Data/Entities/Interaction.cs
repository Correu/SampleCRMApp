namespace SampleCRMApp.Data.Entities;

public class Interaction
{
    public Guid InteractionId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? OpportunityId { get; set; }
    public Guid UserId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }

    public Customer Customer { get; set; } = null!;
    public Opportunity? Opportunity { get; set; }
    public User User { get; set; } = null!;
}
