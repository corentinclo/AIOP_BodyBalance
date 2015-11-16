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
    /// <summary>
    /// Manage managers
    /// </summary>
    [Authorize]
    public class ManagersController : ApiController
    {
        private IManagerServices managerServices;
        private IUserServices userServices;
        public ManagersController(IManagerServices managerServices,
            IUserServices user)
        {
            this.managerServices = managerServices;
            this.userServices = user;
        }

        // GET: Managers
        /// <summary>
        /// Get all managers
        /// </summary>
        /// <returns></returns>
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

            var managers = managerServices.FindAllManagers();
            return Ok(managers);
        }

        // GET: Managers/{manager_id}
        /// <summary>
        /// Retrieves information about a manager
        /// </summary>
        /// <param name="manager_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Managers/{manager_id}")]
        public IHttpActionResult Get(string manager_id)
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

            var manager = managerServices.FindManagerById(manager_id);

            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        // POST: /Managers
        /// <summary>
        /// Create a manager
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody] ManagerModel model)
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
                return BadRequest("Invalid manager supplied");
            }

            var createResult = managerServices.CreateManager(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Manager created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The manager already exists");
            }
            return InternalServerError();
        }

        // DELETE: Managers/{manager_id}
        /// <summary>
        /// Delete a manager
        /// </summary>
        /// <param name="manager_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Managers/{manager_id}")]
        public IHttpActionResult Delete(string manager_id)
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

            var manager = managerServices.FindManagerById(manager_id);
            if (manager == null)
            {
                return NotFound();
            }

            var deleteResult = managerServices.DeleteManager(manager);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }
    }
}
