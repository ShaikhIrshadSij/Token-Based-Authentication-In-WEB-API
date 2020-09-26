using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TokenBasedAuthenticationWebAPI.POCO.Entity;

namespace TokenBasedAuthenticationWebAPI.Models
{
    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (APIContext _apiContext = new APIContext())
            {
                var isUserExist = _apiContext.APIUsers.FirstOrDefault(x => x.Email == context.UserName && x.Password == context.Password);
                if (isUserExist == null)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("Email", context.UserName));
                context.Validated(identity);
            }
        }
    }
}