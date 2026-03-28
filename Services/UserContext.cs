using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace SampleCRMApp.Services;

public class UserContext(AuthenticationStateProvider authenticationStateProvider) : IUserContext
{
    public async Task<Guid?> GetUserIdAsync(CancellationToken cancellationToken = default)
    {
        var state = await authenticationStateProvider.GetAuthenticationStateAsync();
        cancellationToken.ThrowIfCancellationRequested();
        var id = state.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(id, out var guid) ? guid : null;
    }
}
