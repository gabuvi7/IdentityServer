using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCClient.Interfaces;
using MVCClient.Models;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetAuthorization _getAuthorization;

        public HomeController(ILogger<HomeController> logger, IGetAuthorization getAuthorization)
        {
            _logger = logger;
            _getAuthorization = getAuthorization;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = await _getAuthorization.GetTokenFromIdentityServer();
                var response = await _getAuthorization.GetAuthorizationFromAPI(token);
                if (!response.IsSuccessStatusCode)
                {
                    throw new System.ArgumentException(response.StatusCode.ToString());
                }
                var content = await response.Content.ReadAsStringAsync();
                return View(new AuthOrErrorViewModel { ApiResponse = content, Token = response.RequestMessage.Headers.Authorization });
            }
            catch (Exception err)
            {
                return View(new AuthOrErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ApiResponse = "Api NO Autorizada" });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new AuthOrErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
