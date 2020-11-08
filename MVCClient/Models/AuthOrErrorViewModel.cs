using System;
using System.Net.Http.Headers;

namespace MVCClient.Models
{
    public class AuthOrErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ApiResponse { get; set; }
        public AuthenticationHeaderValue Token { get; set; }
    }
}
