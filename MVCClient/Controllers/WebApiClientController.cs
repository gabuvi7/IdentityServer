using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using MVCClient.Interfaces;

namespace MVCClient.Controllers
{
    public class WebApiClientController : Controller
    {
        private readonly IGetAuthorization _getAuthorization;
        public WebApiClientController(IGetAuthorization token)
        {
            _getAuthorization = token;
        }
        public async Task Index()
        {
            try
            {
                var tokenResponse = await _getAuthorization.GetTokenFromIdentityServer();

                if (tokenResponse.IsError)
                {
                    var tokenError = tokenResponse.Error;
                    throw new System.ArgumentException(tokenError);
                }

                var response = await _getAuthorization.GetAuthorizationFromAPI(tokenResponse);
                if (!response.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException(response.StatusCode.ToString());
                }

                Response.Redirect("https://localhost:44392/login");
            }
            catch (Exception err)
            {
                Response.Redirect("https://localhost:44392/Home/Error");
            }
        }
    }
}
