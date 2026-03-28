using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace SampleCRMApp.Services;

public class CookieAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = httpContextAccessor.HttpContext?.User
                   ?? new ClaimsPrincipal(new ClaimsIdentity());
        return Task.FromResult(new AuthenticationState(user));
    }
}
