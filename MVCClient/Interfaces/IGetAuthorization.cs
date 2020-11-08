using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;

namespace MVCClient.Interfaces
{
    public interface IGetAuthorization
    {
        Task<TokenResponse> GetTokenFromIdentityServer();

        Task<HttpResponseMessage> GetAuthorizationFromAPI(TokenResponse tokenResponse);
    }
}
