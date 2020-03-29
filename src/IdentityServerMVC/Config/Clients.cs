
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerMVC.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    RequireConsent = false,
                    ClientId = "angular_spa",
                    ClientName = "Vega Angular App",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "api.read" },
                    RedirectUris = { "https://localhost:5001" },
                    PostLogoutRedirectUris = { "https://localhost:5001" },
                    AllowedCorsOrigins = { "https://localhost:5001" },
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 3600
                }
            };
        }
    }
}
