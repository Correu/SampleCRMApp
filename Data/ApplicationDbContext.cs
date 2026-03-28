using Microsoft.EntityFrameworkCore;
using SampleCRMApp.Data.Entities;

namespace SampleCRMApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Opportunity> Opportunities => Set<Opportunity>();
    public DbSet<Interaction> Interactions => Set<Interaction>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<TicketResponse> TicketResponses => Set<TicketResponse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(e =>
        {
            e.ToTable("Customers");
            e.HasKey(x => x.CustomerId);
            e.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            e.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(320).IsRequired();
            e.Property(x => x.Phone).HasMaxLength(50);
            e.Property(x => x.CompanyName).HasMaxLength(200);
            e.Property(x => x.CreatedAt).HasColumnType("datetime2");
            e.Property(x => x.UpdatedAt).HasColumnType("datetime2");
        });

        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users");
            e.HasKey(x => x.UserId);
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.Email).HasMaxLength(320).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Role).HasMaxLength(50).IsRequired();
            e.Property(x => x.Active);
            e.Property(x => x.CreatedAt).HasColumnType("datetime2");
            e.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
        });

        modelBuilder.Entity<Opportunity>(e =>
        {
            e.ToTable("Opportunities");
            e.HasKey(x => x.OpportunityId);
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.Property(x => x.Status).HasMaxLength(50).IsRequired();
            e.Property(x => x.ExpectedValue).HasPrecision(18, 2);
            e.Property(x => x.CreatedAt).HasColumnType("datetime2");
            e.Property(x => x.UpdatedAt).HasColumnType("datetime2");
            e.HasOne(x => x.Customer).WithMany(c => c.Opportunities).HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.AssignedRep).WithMany(u => u.AssignedOpportunities).HasForeignKey(x => x.AssignedRepId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Interaction>(e =>
        {
            e.ToTable("Interactions");
            e.HasKey(x => x.InteractionId);
            e.Property(x => x.Type).HasMaxLength(50).IsRequired();
            e.Property(x => x.Subject).HasMaxLength(300).IsRequired();
            e.Property(x => x.Details).HasMaxLength(4000);
            e.Property(x => x.Timestamp).HasColumnType("datetime2");
            e.HasOne(x => x.Customer).WithMany(c => c.Interactions).HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Opportunity).WithMany(o => o.Interactions).HasForeignKey(x => x.OpportunityId)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.User).WithMany(u => u.Interactions).HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SupportTicket>(e =>
        {
            e.ToTable("SupportTickets");
            e.HasKey(x => x.TicketId);
            e.Property(x => x.Subject).HasMaxLength(300).IsRequired();
            e.Property(x => x.Description).HasMaxLength(4000);
            e.Property(x => x.Status).HasMaxLength(50).IsRequired();
            e.Property(x => x.Priority).HasMaxLength(50).IsRequired();
            e.Property(x => x.CreatedAt).HasColumnType("datetime2");
            e.Property(x => x.UpdatedAt).HasColumnType("datetime2");
            e.Property(x => x.ClosedAt).HasColumnType("datetime2");
            e.HasOne(x => x.Customer).WithMany(c => c.SupportTickets).HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.AssignedAgent).WithMany(u => u.AssignedSupportTickets).HasForeignKey(x => x.AssignedAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TicketResponse>(e =>
        {
            e.ToTable("TicketResponses");
            e.HasKey(x => x.ResponseId);
            e.Property(x => x.Message).HasMaxLength(4000).IsRequired();
            e.Property(x => x.Timestamp).HasColumnType("datetime2");
            e.HasOne(x => x.Ticket).WithMany(t => t.TicketResponses).HasForeignKey(x => x.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany(u => u.TicketResponses).HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
