using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using WebAPIWithCoreMVC.Exceptions;

namespace WebAPIWithCoreMVC.Handler
{
    public class AuthTokenHandler : DelegatingHandler
    {
        public AuthTokenHandler()
        {

        }
        private IHttpContextAccessor _httpContextAccessor;
        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string token = _httpContextAccessor.HttpContext.Session.GetString("token");
                if (/*request.Headers.Contains("Authorization") &&*/ !String.IsNullOrEmpty(token))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                }
            }
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }
            return response;
        }
    }
}
