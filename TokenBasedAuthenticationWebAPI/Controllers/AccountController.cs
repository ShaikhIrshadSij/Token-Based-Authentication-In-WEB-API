using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TokenBasedAuthenticationWebAPI.POCO.Entity;

namespace TokenBasedAuthenticationWebAPI.Controllers
{
    public class AccountController : ApiController
    {
        private readonly APIContext _apiContext = null;
        public AccountController()
        {
            _apiContext = new APIContext();
        }

        [Route("api/account/RegisterUser")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult RegisterUser()
        {
            try
            {
                HttpRequest httpRequest = HttpContext.Current.Request;
                var form = httpRequest.Form;
                string email = form["Email"];
                string password = form["Password"];
                var isUserExist = _apiContext.APIUsers.FirstOrDefault(x => x.Email == email);
                if (isUserExist == null)
                {
                    isUserExist = new APIUsers
                    {
                        Email = email,
                        Password = password
                    };
                    _apiContext.APIUsers.Add(isUserExist);
                    _apiContext.SaveChanges();
                    return Ok(new
                    {
                        isSuccess = true
                    });
                }
                return Ok(new
                {
                    isSuccess = false,
                    message = "User already exist"
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    isSuccess = false,
                    message = ex.Message
                });
            }
        }
    }
}
