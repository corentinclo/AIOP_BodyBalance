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
        private IBasketServices basketServices;

        public UsersController(IUserServices user,ITokenServices token, IBasketServices basket)
        {
            this.userServices = user;
            this.tokenServices = token;
            this.basketServices = basket;
        }

        // GET: /Users
        [HttpGet]
        [Route("Users")]
        public IHttpActionResult Get()
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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
            if (!(userServices.IsAdmin(userPermission)) && userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: /Users
        [HttpPost]
        [Route("Users")]
        public IHttpActionResult Post([FromBody]UserModel user)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) && userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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

        // DELETE: /Users/{userid}
        [HttpDelete]
        [Route("Users/{userid}")]
        public IHttpActionResult Delete(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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

        // GET: /Users/{userid}/Products
        [HttpGet]
        [Route("Users/{userid}/Products")]
        public IHttpActionResult GetProducts(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listProducts = userServices.FindAllProductsOfUser(userid);

            return Ok(listProducts);
        }

        // GET: /Users/{userid}/Notifications
        [HttpGet]
        [Route("Users/{userid}/Notifications")]
        public IHttpActionResult GetNotifications(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listNotifications = userServices.FindAllNotificationssOfUser(userid);

            return Ok(listNotifications);
        }

        // GET: /Users/{userid}/Baskets
        [HttpGet]
        [Route("Users/{userid}/Baskets")]
        public IHttpActionResult GetBaskets(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listBaskets = userServices.FindBasketOfUser(userid);

            return Ok(listBaskets);
        }

        // POST: /Users/{userid}/Baskets
        [HttpPost]
        [Route("Users/{userid}/Baskets")]
        public IHttpActionResult PostBasketLine(string userid, [FromBody]BasketModel basket)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != basket.UserId)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid basket line supplied");
            }

            var createResult = basketServices.CreateBasketLine(basket);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Basket line created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The basket line already exists");
            }
            return InternalServerError();
        }

        // POST: /Users/{userid}/Baskets/Validate
        [HttpPost]
        [Route("Users/{userid}/Baskets/Validate")]
        public IHttpActionResult PostPurchase(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }

            var createResult = userServices.CreateUserPurchase(userid);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Basket created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The purchase already exists");
            }
            return InternalServerError();
        }

        // GET: /Users/{userid}/Baskets/{productid}
        [HttpGet]
        [Route("Users/{userid}/Baskets/{productid}")]
        public IHttpActionResult GetBasketLine(string userid, string productid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var basketLine = this.basketServices.FindBasketLineWithIds(userid, productid);

            if (basketLine == null)
            {
                return NotFound();
            }
            return Ok(basketLine);
        }

        // PUT: /Users/{userid}/Baskets/{productid}
        [HttpPut]
        [Route("Users/{userid}/Baskets/{productid}")]
        public IHttpActionResult PutBasketLine(string userid, string productid, [FromBody]BasketModel basket)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid basket line supplied");
            }

            var basektLine = basketServices.FindBasketLineWithIds(userid, productid);

            if (basektLine == null)
            {
                return NotFound();
            }
            if (basektLine.UserId != basket.UserId)
            {
                return BadRequest("Invalid user id supplied");
            }
            if (basektLine.ProductId != basket.ProductId)
            {
                return BadRequest("Invalid product id supplied");
            }

            var updateResult = basketServices.UpdateBasketLine(basket);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Basket line updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your user id and product id exist");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: /Users/{userid}/Baskets/{productid}
        [HttpDelete]
        [Route("Users/{userid}/Baskets/{productid}")]
        public IHttpActionResult DeleteBasketLine(string userid, string productid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var basketLine = basketServices.FindBasketLineWithIds(userid, productid);
            if (basketLine == null)
            {
                return BadRequest("Bad user id and/or product id supplied");
            }

            var deleteResult = basketServices.DeleteBasketLine(basketLine);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Basket line deleted successfully");
            }
            return InternalServerError();
        }

        // DELETE: /Users/{user-id}/Baskets
        [HttpDelete]
        [Route("Users/{userid}/Baskets")]
        public IHttpActionResult DeleteBaskets(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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

        // GET: /Users/{userid}/Purchases
        [HttpGet]
        [Route("Users/{userid}/Purchases")]
        public IHttpActionResult GetPurchases(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) && userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listPurchases = userServices.FindAllPurchasesOfUser(userid);

            return Ok(listPurchases);
        }

        // GET: /Users/{userid}/Events
        [HttpGet]
        [Route("Users/{userid}/Events")]
        public IHttpActionResult GetEvents(string userid)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (userPermission.UserId != userid)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(userid);
            if (user == null)
            {
                return BadRequest("Bad user id supplied");
            }
            var listEvents = userServices.FindAllEventsOfUser(userid);

            return Ok(listEvents);
        }
    }
}
