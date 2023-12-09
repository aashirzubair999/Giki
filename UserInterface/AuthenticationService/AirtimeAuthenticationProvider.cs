using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Model.Models;

namespace UserInterface.AuthenticationService;

public class TruckerAuthenticationProvider : AuthenticationStateProvider, ILoginService
{
    private readonly ProtectedSessionStorage _sessionStorage;
    public readonly HttpClient httpClient;
    private AuthenticationState Anonymous => new (new ClaimsPrincipal(new ClaimsIdentity()));

    public TruckerAuthenticationProvider(ProtectedSessionStorage sessionStorage, HttpClient httpClient)
    {
        _sessionStorage = sessionStorage;
        this.httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
            var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
            if (userSession == null)
            {
                return await Task.FromResult(Anonymous);
            }
            else
            {
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new(ClaimTypes.Name, userSession.Name),
                    new(ClaimTypes.Role, userSession.Role),
                    new(ClaimTypes.Email, userSession.Email),
                    
                }, "TruckerAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("there was an error" + e.Message + e.Data);
            return await Task.FromResult(Anonymous);
        }
        
    }

    public async Task Login(UserSession userSession)
    {
        await _sessionStorage.SetAsync("UserSession", userSession);
        var claimsPrincipal = new ClaimsPrincipal(
            new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Name, userSession.Name),
                new(ClaimTypes.Role, userSession.Role),
                new(ClaimTypes.Email, userSession.Email),
            },"AirtimeAuth"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }



    public async Task Logout()
    {   await _sessionStorage.DeleteAsync("UserSession");
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
    }
    

}