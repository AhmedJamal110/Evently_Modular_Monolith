
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class KeyCloakAuthDelegatingHandler(
    IOptions<KeyCloakOptions> options) : DelegatingHandler
{
    private readonly KeyCloakOptions _options = options.Value;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        AuthToken authToken = await GetAuthorizationTokenAsync(cancellationToken);

         request.Headers.Authorization  = new AuthenticationHeaderValue("Bearer", authToken.AccessToken);


        HttpResponseMessage httpResponseMessage =
            await base.SendAsync(request, cancellationToken);
        
        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;

    }

    private async Task<AuthToken> GetAuthorizationTokenAsync(
        CancellationToken cancellationToken)
    {
        var keyValuePairs = new KeyValuePair<string, string?>[]
        {
            new("client_id", _options.ConfidentiatClientId),
            new("client_secret", _options.ConfidentiatClientSecret),
            new("scope", "openid"),
            new("grant_type","client_credentials")

        };

        using var formUrlEncodedContent = new FormUrlEncodedContent(keyValuePairs);

        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _options.TokenUrl);

        httpRequestMessage.Content = formUrlEncodedContent;

        using HttpResponseMessage httpResponseMessage =
           await base.SendAsync(httpRequestMessage, cancellationToken);

        httpResponseMessage.EnsureSuccessStatusCode();

        AuthToken? authToken = await httpResponseMessage.Content
            .ReadFromJsonAsync<AuthToken>(cancellationToken: cancellationToken);

        return authToken;
    }

}



internal sealed class AuthToken
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}
