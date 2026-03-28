namespace SampleCRMApp.Data.Entities;

public class Opportunity
{
    public Guid OpportunityId { get; set; }
    public Guid CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal ExpectedValue { get; set; }
    public DateOnly? CloseDate { get; set; }
    public Guid? AssignedRepId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Customer Customer { get; set; } = null!;
    public User? AssignedRep { get; set; }
    public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();
}
