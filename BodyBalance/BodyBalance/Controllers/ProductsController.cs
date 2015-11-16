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
        private IUserServices userServices;

        public ProductsController(IProductServices productServices,
            IUserServices userServices)
        {
            this.productServices = productServices;
            this.userServices = userServices;
        }
        // GET: Products
        [HttpGet]
        public IHttpActionResult Get()
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            /************************/

            var listProducts = productServices.FindAllProducts();
            return Ok(listProducts);
        }

        // GET: Products/{product_id}
        [HttpGet]
        [Route("Products/{product_id}")]
        public IHttpActionResult Get(string product_id)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            /************************/

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
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (model.UserId != User.Identity.Name)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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
        [Route("Products/{product_id}")]
        public IHttpActionResult Put(string product_id, [FromBody]ProductModel model)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (model.UserId != User.Identity.Name)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

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
        [Route("Products/{product_id}")]
        public IHttpActionResult Delete(string product_id)
        {
            var product = productServices.FindProductWithId(product_id);
            if (product == null)
            {
                return BadRequest("Bad product id supplied");
            }

            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (product.UserId != User.Identity.Name)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/
            
            var deleteResult = productServices.DeleteProduct(product);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Product deleted successfully");
            }
            return InternalServerError();
        }
    }
}
