using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4;

public static class Configuration
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "userRoles.Scope",
                    UserClaims =
                    {
                        "role"
                    }
                }
        };

    public static IEnumerable<ApiResource> GetApis() =>
        new List<ApiResource> {
                new ApiResource("ManagersAPI"),
                new ApiResource("BuyersAPI"),
        };

    public static IEnumerable<Client> GetClients() =>
        new List<Client> {
                new Client {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "ManagersAPI" }
                },
                new Client {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:7066/signin-oidc" },

                    AllowedScopes = {
                        "ManagersAPI",
                        "BuyersAPI",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "userRoles.Scope",
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                }
        };
}