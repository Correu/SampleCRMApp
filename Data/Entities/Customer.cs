namespace SampleCRMApp.Data.Entities;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
    public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}
