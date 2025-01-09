using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityExperiment
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => 
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        ];

        public static IEnumerable<ApiScope> ApiScopes => [
            new(name: "weather-forecast", displayName: "Weather Forecast")
        ];

        public static IEnumerable<Client> Clients => [
            new()
            {
                ClientId = "my-weather-app",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = [new("my-super-secret".Sha256())],
                AllowedScopes = ["weather-forecast"]
            },
            new()
            {
                ClientId = "weather-web-app",
                ClientSecrets = [new("my-super-secret".Sha256())],
                /*
                 * Code: Short for AuthorizationCode
                 * This flow uses back-channel requests to keep the token out of the browser's reach.
                 */
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = ["https://localhost:5002/signin-oidc"],
                PostLogoutRedirectUris = ["https://localhost:5002/signout-callback-oidc"],
                AllowedScopes =
                [
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                ],
            }
        ];
    }
}