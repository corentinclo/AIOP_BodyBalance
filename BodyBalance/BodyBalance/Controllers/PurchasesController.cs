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
    public class PurchasesController : ApiController
    {
        private IPurchaseServices purchaseServices;
        private IPurchaseLineServices purchaseLineServices;

        public PurchasesController(IPurchaseServices purchaseServices, IPurchaseLineServices purchaseLineServices)
        {
            this.purchaseServices = purchaseServices;
            this.purchaseLineServices = purchaseLineServices;
        }

        // GET: /Purchases
        [HttpGet]
        [Route("Purchases")]
        public IHttpActionResult Get()
        {
            var listPurchases = purchaseServices.FindAllPurchases();
            return Ok(listPurchases);
        }

        // GET: /Purchases/{purchaseid}
        [HttpGet]
        [Route("Purchases/{purchaseid}")]
        public IHttpActionResult Get(string purchaseid)
        {
            PurchaseModel purchase = purchaseServices.FindPurchaseWithId(purchaseid);

            if (purchase == null)
            {
                return NotFound();
            }

            purchase.PurchaseLine = purchaseServices.FindAllLinesOfPurchase(purchaseid);
            return Ok(purchase);
        }

        // PUT: /Purchases/{purchaseid}/{productid}
        [HttpPut]
        [Route("Purchases/{purchaseid}/{productid}")]
        public IHttpActionResult Put(string purchaseid, string productid, [FromBody]PurchaseLineModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid purchase line supplied");
            }

            var purchaseLine = purchaseLineServices.FindPurchaseLineWithIds(purchaseid, productid);

            if (purchaseLine == null)
            {
                return NotFound();
            }
            if (purchaseLine.PurchaseId != model.PurchaseId)
            {
                return BadRequest("Invalid purchase id supplied");
            }
            if (purchaseLine.ProductId != model.ProductId)
            {
                return BadRequest("Invalid product id supplied");
            }

            var updateResult = purchaseLineServices.UpdatePurchaseLine(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Purchase line updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your purchase id and product id exist");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: /Purchases/{purchaseid}
        [HttpDelete]
        [Route("Purchases/{purchaseid}")]
        public IHttpActionResult Delete(string purchaseid)
        {
            var purchase = purchaseServices.FindPurchaseWithId(purchaseid);
            if (purchase == null)
            {
                return BadRequest("Bad purchase id supplied");
            }

            var deleteResult = purchaseServices.DeletePurchase(purchase);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Purchase deleted successfully");
            }
            return InternalServerError();
        }

        // DELETE: /Purchases/{purchaseid}/{productid}
        [HttpDelete]
        [Route("Purchases/{purchaseid}/{productid}")]
        public IHttpActionResult DeleteLine(string purchaseid, string productid)
        {
            var purchaseLine = purchaseLineServices.FindPurchaseLineWithIds(purchaseid, productid);
            if (purchaseLine == null)
            {
                return BadRequest("Bad purchase id and/or product id supplied");
            }

            var deleteResult = purchaseLineServices.DeletePurchaseLine(purchaseLine);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Purchase line deleted successfully");
            }
            return InternalServerError();
        }
    }
}