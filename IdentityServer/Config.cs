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
            // TODO: rename to "weather-forecast-api"
            // TODO: Consider extracting it to a common constant
            new(name: "weather-forecast", displayName: "Weather Forecast") 
        ];

        public static IEnumerable<Client> Clients => [
            new()
            {
                ClientId = "my-weather-app", // TODO: Consider extracting it to a common constant
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // TODO: Consider extracting it to a common constant
                // TODO: Consider an extension to call either Sha256|512 so it affects all at the same time
                ClientSecrets = [new("my-super-secret".Sha256())], 
                AllowedScopes = ["weather-forecast"] // TODO: Consider extracting it to a common constant
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
                AllowOfflineAccess = true, // Enables refresh tokens
                AllowedScopes =
                [
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "weather-forecast" // TODO: Consider extracting it to a common constant
                ],
            }
        ];
    }
}