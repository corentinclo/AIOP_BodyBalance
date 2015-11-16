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
    public class BasketsController : ApiController
    {
        private IBasketServices basketServices;

        public BasketsController(IBasketServices basketServices)
        {
            this.basketServices = basketServices;
        }

        // GET: /Baskets/{userid}/{productid}
        [HttpGet]
        [Route("Baskets/{userid}/{productid}")]
        public IHttpActionResult Get(string userid, string productid)
        {
            var basketLine = this.basketServices.FindBasketLineWithIds(userid, productid);

            if (basketLine == null)
            {
                return NotFound();
            }
            return Ok(basketLine);
        }

        // POST: /Baskets
        [HttpPost]
        [Route("Baskets")]
        public IHttpActionResult Post([FromBody]BasketModel basket)
        {
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

        // PUT: /Baskets/{userid}/{productid}
        [HttpPut]
        [Route("Baskets/{userid}/{productid}")]
        public IHttpActionResult Put(string userid, string productid, [FromBody]BasketModel basket)
        {
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
                return Ok("Notification updated successfully");
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

        // DELETE: /Baskets/{userid}/{productid}
        [HttpDelete]
        [Route("Baskets/{userid}/{productid}")]
        public IHttpActionResult Delete(string userid, string productid)
        {
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
    }
}
