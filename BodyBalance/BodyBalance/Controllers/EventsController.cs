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
    /// Manage events
    /// </summary>
    [Authorize]
    public class EventsController : ApiController
    {
        private IEventServices eventServices;
        private IUserServices userServices;

        public EventsController(IEventServices eventServices, 
            IUserServices userServices)
        {
            this.eventServices = eventServices;
            this.userServices = userServices;
        }

        // GET: /Events
        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns> List of events</returns>
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
            var listEvents = eventServices.FindAllEvents();
            return Ok(listEvents);
        }

        // GET: /EventTypes
        /// <summary>
        /// Get all event types
        /// </summary>
        /// <returns> List of event types</returns>
        [HttpGet]
        [Route("EventTypes")]
        public IHttpActionResult GetTypes()
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            /************************/
            var listTypes = eventServices.FindAllTypes();
            return Ok(listTypes);
        }

        // POST: /Events
        /// <summary>
        /// Create an event
        /// </summary>
        /// <param name="model"> event model </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]EventModel model)
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
                return BadRequest("Invalid event supplied");
            }
            
            var createResult = eventServices.CreateEvent(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Event created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The event already exists or bad roomId, activityid, eventtype, contributorid or managerid");
            }
            return InternalServerError();
        }

        // GET: /Events/{event_id}
        /// <summary>
        /// Retrieves information about an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Events/{event_id}")]
        public IHttpActionResult Get(string event_id)
        {
            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            /************************/

            var myEvent = eventServices.FindEventById(event_id);

            if (myEvent == null)
            {
                return NotFound();
            }
            return Ok(myEvent);
        }

        // PUT: api/Events/{event_id}
        /// <summary>
        /// Update an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Events/{event_id}")]
        public IHttpActionResult Put(string event_id, [FromBody]EventModel model)
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
                return BadRequest("Invalid event supplied");
            }

            var myEvent = eventServices.FindEventById(event_id);

            if (myEvent == null)
            {
                return NotFound();
            }
            if (myEvent.EventId != model.EventId)
            {
                return BadRequest("Invalid event id supplied");
            }

            var updateResult = eventServices.UpdateEvent(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Event updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your ActivityId exists");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: api/Events/{event_id}
        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Events/{event_id}")]
        public IHttpActionResult Delete(string event_id)
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

            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return NotFound();
            }

            var deleteResult = eventServices.DeleteEvent(myEvent);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Event deleted successfully");
            }
            return InternalServerError();
        }

        // GET: /Events/{event_id}/Users
        /// <summary>
        /// Retrieves all users of an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [Route("Events/{event_id}/Users")]
        [HttpGet]
        public IHttpActionResult GetUsers(string event_id)
        {
            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return NotFound();
            }

            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) && myEvent.ManagerId != user.UserId && myEvent.ContributorId != user.UserId )
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            

            var listUsers = eventServices.FindUsersOfEvent(event_id);

            return Ok(listUsers);
        }

        // GET: /Events/{event_id}/Contributor
        /// <summary>
        /// Retrieves the contributor of an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [Route("Events/{event_id}/Contributor")]
        [HttpGet]
        public IHttpActionResult GetContributor(string event_id)
        {
            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return NotFound();
            }

            /** Check Permissions **/
            var user = userServices.FindUserById(User.Identity.Name);
            if (user == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(user)) && myEvent.ManagerId != user.UserId )
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var contributor = eventServices.FindContributorOfEvent(event_id);

            return Ok(contributor);
        }

        // GET: /Events/{event_id}/Manager
        /// <summary>
        /// Retrieves the manager of an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [Route("Events/{event_id}/Manager")]
        [HttpGet]
        public IHttpActionResult GetManager(string event_id)
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

            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return NotFound();
            }

            var manager = eventServices.FindManagerOfEvent(event_id);

            return Ok(manager);
        }

        // POST: Events/{event_id}/RegisterUser
        /// <summary>
        /// Register a user to an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Events/{event_id}/RegisterUser")]
        public IHttpActionResult RegisterUserToEvent(string event_id, [FromBody] string user_id )
        {
            var user = userServices.FindUserById(user_id);
            if (user == null)
            {
                return BadRequest("Bad user id");
            }

            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) && userPermission.UserId != user_id)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            
            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return BadRequest("Bad event id");
            }

            var registerResult = eventServices.RegisterUserToEvent(event_id, user);
            if (registerResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("User registered sucessfully");
            }
            if (registerResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The user is already register to this event");
            }
            return InternalServerError();
        }

        // DELETE: Events/{event_id}/RemoveUser
        /// <summary>
        /// Delete a user from an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Events/{event_id}/Users/{user_id}")]
        public IHttpActionResult RemoveUserToEvent(string event_id, string user_id)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) && userPermission.UserId != user_id)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var user = userServices.FindUserById(user_id);
            if (user == null)
            {
                return BadRequest("Bad user id");
            }
            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return BadRequest("Bad event id");
            }

            var deleteResult = eventServices.RemoveUserOfEvent(event_id, user);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("User removed sucessfully");
            }
            return InternalServerError();
        }

        // GET: Events/{event_id}/IsRegisteredUser/{user_id}
        /// <summary>
        /// To know if a user is register to an event
        /// </summary>
        /// <param name="event_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Events/{event_id}/IsRegisteredUser/{user_id}")]
        public IHttpActionResult IsRegisteredUser(string event_id, string user_id)
        {
            var user = userServices.FindUserById(user_id);
            if (user == null)
            {
                return BadRequest("Bad user id");
            }

            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) && userPermission.UserId != user_id)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/


            var myEvent = eventServices.FindEventById(event_id);
            if (myEvent == null)
            {
                return BadRequest("Bad event id");
            }

            var userRegistered = eventServices.FindOneUserOfEvent(event_id, user_id);
            
            return Ok(userRegistered != null);
        }
    }
}
