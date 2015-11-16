using BodyBalance.Models;
using BodyBalance.Services;
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

        public PurchasesController(IPurchaseServices purchaseServices)
        {
            this.purchaseServices = purchaseServices;
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

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}