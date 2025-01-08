using Duende.IdentityServer.Models;

namespace IdentityExperiment
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => [
            new IdentityResources.OpenId()
        ];

        public static IEnumerable<ApiScope> ApiScopes => [
            new(name: "weather-forecast", displayName: "Weather Forecast")
        ];

        public static IEnumerable<Client> Clients => [
            new()
            {
                ClientId = "my-weather-app",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = [
                    new("my-super-secret".Sha256())
                ],
                AllowedScopes = ["weather-forecast"]
            }
        ];
    }
}