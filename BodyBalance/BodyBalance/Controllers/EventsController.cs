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
    public class EventsController : ApiController
    {
        private IEventServices eventServices;

        public EventsController(IEventServices eventServices)
        {
            this.eventServices = eventServices;
        }

        // GET: /Events
        [HttpGet]
        public IHttpActionResult Get()
        {
            var listEvents = eventServices.FindAllEvents();
            return Ok(listEvents);
        }

        // POST: api/Events
        [HttpPost]
        public IHttpActionResult Post([FromBody]EventModel model)
        {
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

        // GET: api/Events/{event_id}
        [HttpGet]
        public IHttpActionResult Get(string event_id)
        {
            var myEvent = eventServices.FindEventById(event_id);

            if (myEvent == null)
            {
                return NotFound();
            }
            return Ok(myEvent);
        }

        // PUT: api/Events/{event_id}
        [HttpPut]
        public IHttpActionResult Put(string event_id, [FromBody]EventModel model)
        {
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
        [HttpDelete]
        public IHttpActionResult Delete(string event_id)
        {
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
    }
}
