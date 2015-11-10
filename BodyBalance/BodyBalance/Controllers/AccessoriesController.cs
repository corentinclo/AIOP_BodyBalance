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
        public AccessoriesController(IAccessoryServices accessoryServices)
        {
            this.accessoryServices = accessoryServices;
        }

        // GET: api/Accessories
        [HttpGet]
        public IHttpActionResult Get()
        {
            var listAccessories = accessoryServices.FindAllAccessories();
            return Ok(listAccessories);
        }

        // GET: api/Accessories/{accessory_id}
        [HttpGet]
        public IHttpActionResult Get(string accessory_id)
        {
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
        public IHttpActionResult Put(string accessory_id, [FromBody]AccessoryModel model)
        {
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
        public IHttpActionResult Delete(string accessory_id)
        {
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
