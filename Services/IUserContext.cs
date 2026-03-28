namespace SampleCRMApp.Services;

public interface IUserContext
{
    Task<Guid?> GetUserIdAsync(CancellationToken cancellationToken = default);
}
