using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleCRMApp.Data.Entities;
using SampleCRMApp;

namespace SampleCRMApp.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext db, IPasswordHasher<User> passwordHasher, CancellationToken cancellationToken = default)
    {
        await db.Database.MigrateAsync(cancellationToken);
        if (await db.Users.AnyAsync(cancellationToken))
            return;

        var admin = new User
        {
            UserId = Guid.NewGuid(),
            Name = "Administrator",
            Email = "admin@samplecrm.local",
            Role = AppRoles.Admin,
            Active = true,
            CreatedAt = DateTime.UtcNow,
            PasswordHash = string.Empty
        };
        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");
        db.Users.Add(admin);
        await db.SaveChangesAsync(cancellationToken);
    }
}
