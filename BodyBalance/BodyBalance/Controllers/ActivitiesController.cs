using BodyBalance.Models;
using BodyBalance.Services;
using BodyBalance.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BodyBalance.Controllers
{
    /// <summary>
    /// Manage activities 
    /// </summary>
    [Authorize]
    public class ActivitiesController : ApiController
    {
        private IActivityServices activityServices;
        private IUserServices userServices;
        public ActivitiesController(IActivityServices activityServices,
            IUserServices userServices)
        {
            this.activityServices = activityServices;
            this.userServices = userServices;
        }

        // GET: Activities
        /// <summary>
        /// Retrieves all activities
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
            /************************/

            var listActivities = activityServices.FindAllActivities();
            return Ok(listActivities);
        }

        // GET: Activities/{activity_id}
        /// <summary>
        /// Retrieves informations of an activity
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Activities/{activity_id}")]
        public IHttpActionResult Get(string activity_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            /************************/

            var activity = activityServices.FindActivityById(activity_id);

            if(activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        // POST: /Activities
        /// <summary>
        /// Create an activity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]ActivityModel model)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) && !(userServices.IsManager(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid activity supplied");
            }

            var createResult = activityServices.CreateActivity(model);
            if(createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Activity created sucessfully");
            }
            if(createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The activity already exists or The manager doesn't exist");
            }
            return InternalServerError();
        }

        // PUT: /Activities/{activity_id}
        /// <summary>
        /// Update an activity
        /// </summary>
        /// <param name="activity_id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Activities/{activity_id}")]
        public IHttpActionResult Put(string activity_id, [FromBody]ActivityModel model)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) && !(userServices.IsManager(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid activity supplied");
            }

            var activity = activityServices.FindActivityById(activity_id);

            if(activity == null)
            {
                return NotFound();
            }
            if(activity.ActivityId != model.ActivityId)
            {
                return BadRequest("Invalid activity id supplied");
            }

            var updateResult = activityServices.UpdateActivity(model);
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
                var ex = new Exception("Make Sure that your managerId exists");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: /Activities/{activity_id}
        /// <summary>
        /// Delete an activity
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Activities/{activity_id}")]
        public IHttpActionResult Delete(string activity_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) && !(userServices.IsManager(user)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var activity = activityServices.FindActivityById(activity_id);
            if(activity == null)
            {
                return NotFound();
            }

            var deleteResult = activityServices.DeleteActivity(activity);
            if(deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok();
            }

            return InternalServerError();
        }

        // GET: Activities/{activity_id}/Events
        /// <summary>
        /// Get the events of an activity
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Activities/{activity_id}/Events")]
        public IHttpActionResult GetEvents(string activity_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            /************************/

            var activity = activityServices.FindActivityById(activity_id);
            if (activity == null)
            {
                return NotFound();
            }

            var listevents = activityServices.FindAllEventsOfActivity(activity_id);
            return Ok(listevents);
        }
    }
}
