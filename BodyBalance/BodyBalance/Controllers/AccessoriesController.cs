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
    public class AccessoriesController : ApiController
    {

        private IAccessoryServices accessoryServices;
        private IUserServices userServices;
        public AccessoriesController(IAccessoryServices accessoryServices,
            IUserServices userServices)
        {
            this.accessoryServices = accessoryServices;
            this.userServices = userServices;
        }

        // GET: api/Accessories
        [HttpGet]
        public IHttpActionResult Get()
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/
            var listAccessories = accessoryServices.FindAllAccessories();
            return Ok(listAccessories);
        }

        // GET: api/Accessories/{accessory_id}
        [HttpGet]
        [Route("Accessories/{accessory_id}")]
        public IHttpActionResult Get(string accessory_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var accessory = accessoryServices.FindAccessoryById(accessory_id);

            if (accessory == null)
            {
                return NotFound();
            }
            return Ok(accessory);
        }

        // POST: api/Accessories
        [HttpPost]
        public IHttpActionResult Post([FromBody]AccessoryModel model)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid accessory supplied");
            }

            var createResult = accessoryServices.CreateAccessory(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Accessory created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The accessory already exists");
            }
            return InternalServerError();
        }

        // PUT: api/Accessories/{accessory_id}
        [HttpPut]
        [Route("Accessories/{accessory_id}")]
        public IHttpActionResult Put(string accessory_id, [FromBody]AccessoryModel model)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid accessory supplied");
            }

            var accessory = accessoryServices.FindAccessoryById(accessory_id);

            if (accessory == null)
            {
                return NotFound();
            }
            if (accessory.AccessoryId != model.AccessoryId)
            {
                return BadRequest("Invalid accessory id supplied");
            }

            var updateResult = accessoryServices.UpdateAccessory(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Accessory updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            return InternalServerError();
        }

        // DELETE: api/Accessories/{accessory_id}
        [HttpDelete]
        [Route("Accessories/{accessory_id}")]
        public IHttpActionResult Delete(string accessory_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var accessory = accessoryServices.FindAccessoryById(accessory_id);
            if (accessory == null)
            {
                return NotFound();
            }

            var deleteResult = accessoryServices.DeleteAccessory(accessory);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Accessory deleted successfully");
            }
            return InternalServerError();
        }
    }
}
