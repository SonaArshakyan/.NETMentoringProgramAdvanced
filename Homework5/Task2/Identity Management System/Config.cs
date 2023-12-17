using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

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
            ClientId = "client",
            AllowedGrantTypes = { GrantType.ClientCredentials },
            ClientSecrets = { new Secret("client_secret".Sha256()) },
            AllowedScopes = { "read", "create", "update", "delete" },            
        },
    };

    public static IEnumerable<IdentityResource> IdentityResourcesWithRoles =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> { "manager", "buyer" },
            }
        };
    public static List<TestUser> Users =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "password",
                 Claims = new List<Claim>
                 {
                    new Claim("manager", "true"),
                },               

            }
        };
}
