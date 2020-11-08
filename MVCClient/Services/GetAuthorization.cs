using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using MVCClient.Interfaces;

namespace MVCClient.Services
{
    public class GetAuthorization : IGetAuthorization
    {
        public async Task<TokenResponse> GetTokenFromIdentityServer()
        {
            var client = new HttpClient();
            var serverResp = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (serverResp.IsError)
            {
                var error = serverResp.Error;
                //throw new System.ArgumentException(error);
            }
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = serverResp.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });
            return tokenResponse;
        }

        public async Task<HttpResponseMessage> GetAuthorizationFromAPI(TokenResponse tokenResponse)
        {
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("https://localhost:44340/api/identity");
            return response;
        }
    }
}
