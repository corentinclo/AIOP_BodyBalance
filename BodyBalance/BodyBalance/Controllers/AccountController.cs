using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using BodyBalance.Models;
using BodyBalance.Providers;
using BodyBalance.Results;
using BodyBalance.Services;

namespace BodyBalance.Controllers
{
    [Authorize]
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private IUserServices userServices;
        private ITokenServices tokenServices;

        public AccountController()
        {
        }

        public AccountController(
            IUserServices userServices,
            ITokenServices tokenServices)
        {
            this.userServices = userServices;
            this.tokenServices = tokenServices;
        }

        // POST Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public IHttpActionResult Register(UserModel model) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = userServices.CreateUser(model);

            if (!result)
            {
                return BadRequest();
            }

            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var expireDate = DateTime.UtcNow.AddDays(1);

            var createTokenResult = tokenServices.CreateToken(new TokenModel() { ExpireDate = expireDate, Token = token, UserId = model.UserId});
            if (!createTokenResult)
            {
                return BadRequest();
            }
            return Ok(token);
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            //Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
            //    model.NewPassword);
            
            //if (!result.Succeeded)
            //{
            //    //return GetErrorResult(result);
            //}

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            //if (!result.Succeeded)
            //{
            //    return GetErrorResult(result);
            //}

            return Ok();
        }


    }
}
