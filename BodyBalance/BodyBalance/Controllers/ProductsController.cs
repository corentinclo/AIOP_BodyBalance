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
    public class ProductsController : ApiController
    {
        private IProductServices productServices;

        public ProductsController(IProductServices productServices)
        {
            this.productServices = productServices;
        }
        // GET: Products
        [HttpGet]
        public IHttpActionResult Get()
        {
            var listProducts = productServices.FindAllProducts();
            return Ok(listProducts);
        }

        // GET: Products/{product_id}
        [HttpGet]
        public IHttpActionResult Get(string product_id)
        {
            var product = productServices.FindProductWithId(product_id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: Products
        [HttpPost]
        public IHttpActionResult Post([FromBody]ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid product supplied");
            }

            var createResult = productServices.CreateProduct(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Product created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The product already exists or bad user id or category id");
            }
            return InternalServerError();
        }

        // PUT: api/Products/{product_id}
        [HttpPut]
        public IHttpActionResult Put(string product_id, [FromBody]ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid product supplied");
            }

            var product = productServices.FindProductWithId(product_id);

            if (product == null)
            {
                return NotFound();
            }
            if (product.ProductId != model.ProductId)
            {
                return BadRequest("Invalid product id supplied");
            }

            var updateResult = productServices.UpdateProduct(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Product updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your user id and category id exist");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: api/Products/{product_id}
        [HttpDelete]
        public IHttpActionResult Delete(string product_id)
        {
            var product = productServices.FindProductWithId(product_id);
            if (product_id == null)
            {
                return BadRequest("Bad product id supplied");
            }

            var deleteResult = productServices.DeleteProduct(product);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Product deleted successfully");
            }
            return InternalServerError();
        }
    }
}
