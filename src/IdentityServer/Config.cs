// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "Name",
                    UserClaims = new List<string> {"name"}
                },
                new IdentityResource
                {
                    Name = "Surname",
                    UserClaims = new List<string> {"surname"}
                },
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope>
            {
                new ApiScope("api1", "My API"),
                new ApiScope("apiDePruebaConexionSQL", "SQL"),
                new ApiScope("apiBookListMVC","Booklist")
            };

        public static List<ApiResource> ApiResources
        {
            get
            {
                ApiResource apiResource1 = new ApiResource("webApiResource", "My API")
                {
                    Scopes = { "api1" },
                    UserClaims = { "role" },
                    ApiSecrets = { new Secret("ScopeSecret".Sha256()) }
                };

                List<ApiResource> apiResources = new List<ApiResource>();
                apiResources.Add(apiResource1);

                return apiResources;
            }
        }

        public static IEnumerable<Client> Clients =>
          new List<Client>
          {
            new Client
            {
                ClientId = "client",
                ClientName = "Client app using client credentials",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "api1" }
            },
            new Client
            {
                ClientId = "clientBookListMVC",
                ClientName = "Client credentials",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "apiBookListMVC" }
            }
          };
    }
}