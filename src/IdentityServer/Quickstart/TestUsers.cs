// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "calle falsa 123",
                    locality = "Springfield",
                    postal_code = 1234,
                    country = "USA"
                };
                
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "homero",
                        Password = "homero",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Homero Simpsons"),
                            new Claim(JwtClaimTypes.GivenName, "Homero"),
                            new Claim(JwtClaimTypes.FamilyName, "Simpsons"),
                            new Claim(JwtClaimTypes.Email, "Homero@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://homerou.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "88421113",
                        Username = "Bart",
                        Password = "Bart",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bart Simpsons"),
                            new Claim(JwtClaimTypes.GivenName, "Bart"),
                            new Claim(JwtClaimTypes.FamilyName, "Simpsons"),
                            new Claim(JwtClaimTypes.Email, "Bart@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://TheSimpsons.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }
    }
}