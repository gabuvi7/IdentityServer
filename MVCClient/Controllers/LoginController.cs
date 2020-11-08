using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Interfaces;
using MVCClient.Models;

public class LoginController : Controller
{
    private readonly IGetAuthorization _getAuthorization;
    public LoginController(IGetAuthorization authorization)
    {
        _getAuthorization = authorization;
    }
    public async Task<IActionResult> Index()
    {
        try
        {
            var token = await _getAuthorization.GetTokenFromIdentityServer();
            var response = await _getAuthorization.GetAuthorizationFromAPI(token);
            if (!(response.IsSuccessStatusCode))
            {
                throw new System.ArgumentException();
            }
            var content = await response.Content.ReadAsStringAsync();
            return View(new AuthOrErrorViewModel { ApiResponse = content, Token = response.RequestMessage.Headers.Authorization });
        }
        catch (Exception err)
        {
            return View(new AuthOrErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ApiResponse = "Api NO Autorizada" });
        }
    }
}

