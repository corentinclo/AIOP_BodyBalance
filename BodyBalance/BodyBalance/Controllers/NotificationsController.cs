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
    public class NotificationsController : ApiController
    {
        private INotificationServices notificationServices;

        public NotificationsController(INotificationServices notificationServices)
        {
            this.notificationServices = notificationServices;
        }

        // GET: Notifications
        [HttpGet]
        public IHttpActionResult Get()
        {
            var listNotifications = notificationServices.FindAllNotifications();
            return Ok(listNotifications);
        }

        // GET: api/Notifications/{notification_id}
        [HttpGet]
        public IHttpActionResult Get(string notification_id)
        {
            var notification = notificationServices.FindNotificationWithId(notification_id);

            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        // POST: api/Notifications
        [HttpPost]
        public IHttpActionResult Post([FromBody]NotificationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid notification supplied");
            }

            var createResult = notificationServices.CreateNotification(model);
            if (createResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Notification created sucessfully");
            }
            if (createResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                return BadRequest("The notification already exists or bad user id supplied");
            }
            return InternalServerError();
        }

        // PUT: api/Notifications/{notification_id}
        [HttpPut]
        public IHttpActionResult Put(string notification_id, [FromBody]NotificationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid notification supplied");
            }

            var notification = notificationServices.FindNotificationWithId(notification_id);

            if (notification == null)
            {
                return NotFound();
            }
            if (notification.NotificationId != model.NotificationId)
            {
                return BadRequest("Invalid notification id supplied");
            }

            var updateResult = notificationServices.UpdateNotification(model);
            if (updateResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Notification updated successfully");
            }
            if (updateResult == DaoUtilities.DISPOSED_EXCEPTION)
            {
                var ex = new Exception("Connection have been disposed");
                return InternalServerError(ex);
            }
            if (updateResult == DaoUtilities.UPDATE_EXCEPTION)
            {
                var ex = new Exception("Make Sure that your user id exist");
                return InternalServerError(ex);
            }

            return InternalServerError();
        }

        // DELETE: api/Notifications/{notification_id}
        [HttpDelete]
        public IHttpActionResult Delete(string notification_id)
        {
            var notification = notificationServices.FindNotificationWithId(notification_id);
            if (notification == null)
            {
                return BadRequest("Bad notification id supplied");
            }

            var deleteResult = notificationServices.DeleteNotification(notification);
            if (deleteResult == DaoUtilities.SAVE_SUCCESSFUL)
            {
                return Ok("Notification deleted successfully");
            }
            return InternalServerError();
        }
    }
}
