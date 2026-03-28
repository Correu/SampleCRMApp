namespace SampleCRMApp.Data.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    /// <summary>PBKDF2 hash from ASP.NET Core PasswordHasher; never store plain text.</summary>
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
    public ICollection<Opportunity> AssignedOpportunities { get; set; } = new List<Opportunity>();
    public ICollection<SupportTicket> AssignedSupportTickets { get; set; } = new List<SupportTicket>();
    public ICollection<TicketResponse> TicketResponses { get; set; } = new List<TicketResponse>();
}
