using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SampleCRMApp.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=SampleCRMDB;User Id=sa;Password=SampleCRM_Sa1!;TrustServerCertificate=True;MultipleActiveResultSets=true");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
