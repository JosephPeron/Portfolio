using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SecuroteckWebApplication.Models;

namespace SecuroteckWebApplication.Controllers
{
    public class APIAuthorisationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            #region Task5
            // TODO:  Find if a header ‘ApiKey’ exists, and if it does, check the database to determine if the given API Key is valid
            //        Then authorise the principle on the current thread using a claim, claimidentity and claimsprinciple
            #endregion
            //if is apikey 
            if (HttpContext.Current.Request.Headers["apikey"] != null)
            {
                string ApiKey = HttpContext.Current.Request.Headers["apikey"].ToString();
                bool exists = UserDatabaseAccess.CheckAPIKeyExists(ApiKey);
                //if valid
                if (exists)
                {
                    User user = UserDatabaseAccess.CheckAPIReturnUser(ApiKey);
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));

                    var claimsIdentity = new ClaimsIdentity(claims, ApiKey);

                    var principal = new ClaimsPrincipal(new[] { claimsIdentity });
                    Thread.CurrentPrincipal = principal;
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}