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
    /// Manage Room
    /// </summary>
    [Authorize]
    public class RoomsController : ApiController
    {
        private IRoomServices roomServices;
        private IAccessoryServices accessoryServices;
        private IUserServices userServices;
        public RoomsController(IRoomServices roomServices,
            IAccessoryServices accessoryServices,
            IUserServices userServices)
        {
            this.roomServices = roomServices;
            this.accessoryServices = accessoryServices;
            this.userServices = userServices;
        }

        // GET: Rooms
        /// <summary>
        /// Get all rooms
        /// </summary>
        /// <returns></returns>
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

            var listRooms = roomServices.FindAllRooms();
            return Ok(listRooms);
        }

        // GET: Rooms/{room_id}
        /// <summary>
        /// Retrieves information about a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Rooms/{room_id}")]
        public IHttpActionResult Get(string room_id)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);

            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // POST: Rooms
        /// <summary>
        /// Create a room
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody]RoomModel model)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid room supplied");
            }

            var createResult = roomServices.CreateRoom(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Room created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The Room already exists");
            }
            return InternalServerError();
        }

        // PUT: Rooms/{room_id}
        /// <summary>
        /// Update information about a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Rooms/{room_id}")]
        public IHttpActionResult Put(string room_id, [FromBody]RoomModel model)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid room supplied");
            }

            var room = roomServices.FindRoomById(room_id);

            if (room == null)
            {
                return NotFound();
            }
            if (room.RoomId != model.RoomId)
            {
                return BadRequest("Invalid room id supplied");
            }

            var updateResult = roomServices.UpdateRoom(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Room updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            return InternalServerError();
        }

        // DELETE: Rooms/{room_id}
        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Rooms/{room_id}")]
        public IHttpActionResult Delete(string room_id)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);
            if (room == null)
            {
                return NotFound();
            }

            var deleteResult = roomServices.DeleteRoom(room);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Room deleted successfully");
            }
            return InternalServerError();
        }

        // GET: Rooms/{room_id}/Events
        /// <summary>
        /// Get all the events that will take place in a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <returns></returns>
        [Route("Rooms/{room_id}/Events")]
        [HttpGet]
        public IHttpActionResult GetEvents(string room_id)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);
            if (room == null)
            {
                return NotFound();
            }

            var listEvents = roomServices.FindAllEventsOfRoom(room_id);

            return Ok(listEvents);
        }

        // GET: Rooms/{room_id}/Accessories
        /// <summary>
        /// Get all accessories that are in a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Rooms/{room_id}/Accessories")]
        public IHttpActionResult GetAccessories(string room_id)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)) && !(userServices.IsManager(userPermission)) && !(userServices.IsContributor(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);
            if (room == null)
            {
                return NotFound();
            }

            var listAccessories = roomServices.FindAllAccessoriesOfRoom(room_id);

            return Ok(listAccessories);
        }

        // POST: Rooms/{room_id}/Accessories
        /// <summary>
        /// Add accessories in a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Rooms/{room_id}/Accessories")]
        public IHttpActionResult AddAccessories(string room_id, [FromBody] AccessoryModel model)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);
            if (room == null)
            {
                return BadRequest("Invalid room id supplied");
            }

            var accessory = accessoryServices.FindAccessoryById(model.AccessoryId);
            if (accessory == null)
            {
                return BadRequest("Invalid accessory id supplied");
            }

            var addResult = roomServices.AddAccessoryToRoom(room_id, accessory, model.Quantity);

            if (addResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Accessories added to the room successfully");
            }
            if (addResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            return InternalServerError();
        }

        // DELETE: Rooms/{room_id}/Accessories
        /// <summary>
        /// Remove accessories to a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Rooms/{room_id}/Accessories")]
        public IHttpActionResult DeleteAccessories(string room_id, [FromBody] AccessoryModel model)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);
            if (room == null)
            {
                return BadRequest("Invalid room id supplied");
            }

            var accessory = accessoryServices.FindAccessoryById(model.AccessoryId);
            if (accessory == null)
            {
                return BadRequest("Invalid accessory id supplied");
            }

            var removeResult = roomServices.RemoveAccessoryOfRoom(room_id, accessory);

            if (removeResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Accessories removed to the room successfully");
            }
            if (removeResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            return InternalServerError();
        }

        // PUT: Rooms/{room_id}/Accessories
        /// <summary>
        /// Update number of an accessory in a room
        /// </summary>
        /// <param name="room_id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Rooms/{room_id}/Accessories")]
        public IHttpActionResult UpdateAccessories(string room_id, [FromBody] AccessoryModel model)
        {
            /** Check Permissions **/
            var userPermission = userServices.FindUserById(User.Identity.Name);
            if (userPermission == null)
            {
                return Unauthorized();
            }
            if (!(userServices.IsAdmin(userPermission)))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            /************************/

            var room = roomServices.FindRoomById(room_id);
            if (room == null)
            {
                return BadRequest("Invalid room id supplied");
            }

            var accessory = accessoryServices.FindAccessoryById(model.AccessoryId);
            if (accessory == null)
            {
                return BadRequest("Invalid accessory id supplied");
            }

            var removeResult = roomServices.UpdateAccessoryInRoom(room_id, accessory, model.Quantity);

            if (removeResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Accessories updated to the room successfully");
            }
            if (removeResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            return InternalServerError();
        }
    }
}
