using BodyBalance.Models;
using BodyBalance.Services;
using BodyBalance.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private IUserServices userServices;
        private ITokenServices tokenServices;

        
        public UsersController(IUserServices user,ITokenServices token)
        {
            this.userServices = user;
            this.tokenServices = token;
        }

        // GET: /Users
        [HttpGet]
        [Route("Users")]
        public IHttpActionResult Get()
        {
            var users = this.userServices.FindAllUsers();
            return Ok(users);
        }

        // GET: /Users/{userid}
        [HttpGet]
        [Route("Users/{userid}")]
        public IHttpActionResult Get(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) || userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);

            if (user == null)
            {
                return NotFound();
            }

            user.UserRoles.IsAdmin = userServices.IsAdmin(user);
            user.UserRoles.IsContributor = userServices.IsContributor(user);
            user.UserRoles.IsManager = userServices.IsManager(user);
            user.UserRoles.IsMember = userServices.IsMember(user);

            return Ok(user);
        }

        // POST: /Users
        [HttpPost]
        [Route("Users")]
        public IHttpActionResult Post([FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user supplied");
            }

            var createResult = userServices.CreateUser(user);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("User created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The user already exists");
            }
            return InternalServerError();
        }

        // PUT: /Users/{userid}
        [HttpPut]
        [Route("Users/{userid}")]
        public IHttpActionResult Put(string userid, [FromBody]UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid user supplied");
            }

            if (userServices.FindUserByIdAndPassword(user.UserId, user.Password) == null)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }

            var updateResult = userServices.UpdateUser(user);
            
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your userId exists");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: /Users/{user-id}
        [HttpDelete]
        [Route("Users/{userid}")]
        public IHttpActionResult Delete(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return NotFound();
            }

            var deleteResult = userServices.DeleteUser(user);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }

        // GET: /Users/{user-id}/Products
        [HttpGet]
        [Route("Users/{userid}/Products")]
        public IHttpActionResult GetProducts(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listProducts = userServices.FindAllProductsOfUser(userid);

            return Ok(listProducts);
        }

        // GET: /Users/{user-id}/Notifications
        [HttpGet]
        [Route("Users/{userid}/Notifications")]
        public IHttpActionResult GetNotifications(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listNotifications = userServices.FindAllNotificationssOfUser(userid);

            return Ok(listNotifications);
        }

        // GET: /Users/{user-id}/Baskets
        [HttpGet]
        [Route("Users/{userid}/Baskets")]
        public IHttpActionResult GetBaskets(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listBaskets = userServices.FindBasketOfUser(userid);

            return Ok(listBaskets);
        }

        // DELETE: /Users/{user-id}/Baskets
        [HttpDelete]
        [Route("Users/{userid}/Baskets")]
        public IHttpActionResult DeleteBaskets(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return NotFound();
            }

            var deleteResult = userServices.DeleteUserBasket(userid);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }

        // GET: /Users/{user-id}/Purchases
        [HttpGet]
        [Route("Users/{userid}/Purchases")]
        public IHttpActionResult GetPurchases(string userid)
        {
            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listPurchases = userServices.FindAllPurchasesOfUser(userid);

            return Ok(listPurchases);
        }
    }
}
