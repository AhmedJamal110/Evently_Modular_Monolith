namespace Evently.Modules.Users.Infrastructure.Identity;
public sealed class KeyCloakOptions
{
    public string AdminUrl { get; set; }
    public string TokenUrl { get; set; }
    public string ConfidentiatClientId { get; set; }

    public string ConfidentiatClientSecret { get; set; }

    public string PublicClientId { get; set; }
}
