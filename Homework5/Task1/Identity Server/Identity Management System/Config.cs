using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityManagementSystem;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes =>
       new List<ApiScope>
        {
            new ApiScope("read", "Read Scope"),
            new ApiScope("create", "Create Scope"),
            new ApiScope("update", "Update Scope"),
            new ApiScope("delete", "Delete Scope"),
        };

    public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "manager_client",
            AllowedGrantTypes = { GrantType.ClientCredentials },
            ClientSecrets = { new Secret("manager_secret".Sha256()) },
            AllowedScopes = { "read", "create", "update", "delete" },
        },
        new Client
        {
            ClientId = "buyer_client",
            AllowedGrantTypes = { GrantType.ClientCredentials },
            ClientSecrets = { new Secret("buyer_secret".Sha256()) },
            AllowedScopes = { "read" },
        }
    };

    public static IEnumerable<IdentityResource> IdentityResourcesWithRoles =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> { "manager", "buyer" }
            }
        };
    public static List<TestUser> Users =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password"
            }
        };
}
