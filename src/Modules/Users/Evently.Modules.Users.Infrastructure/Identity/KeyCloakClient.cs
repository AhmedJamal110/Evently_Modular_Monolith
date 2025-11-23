using System.Net.Http.Json;

namespace Evently.Modules.Users.Infrastructure.Identity;
internal sealed class KeyCloakClient(HttpClient _httpClient)
{
    internal async Task<string> RegisterUserAsync(
        UserRepersentation userRepersentation,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponseMessage 
            = await _httpClient.PostAsJsonAsync("users", userRepersentation, cancellationToken);
    
        httpResponseMessage.EnsureSuccessStatusCode();

        return ExtractIdentityFromLocationHeader(httpResponseMessage);

    }


    private static string ExtractIdentityFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
      const string usersPathSegment = "users/";

        string pathAndQuery = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (pathAndQuery is null )
        {
            throw new InvalidOperationException("location Header is null");
        }

        int index = pathAndQuery.IndexOf(
            usersPathSegment,
            StringComparison.InvariantCultureIgnoreCase);

        string identityId = pathAndQuery.Substring(
            index, usersPathSegment.Length);


        return identityId;
    }


}
