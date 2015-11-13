using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface INotificationServices
    {
        /// <summary>
        /// Create a notification
        /// </summary>
        /// <param name="nm"></param>
        /// <returns></returns>
        int CreateNotification(NotificationModel nm);

        /// <summary>
        /// Find a notification with its id
        /// </summary>
        /// <param name="NotificationId"></param>
        /// <returns></returns>
        NotificationModel FindNotificationWithId(string NotificationId);

        /// <summary>
        /// Update a notification
        /// </summary>
        /// <param name="nm"></param>
        /// <returns></returns>
        int UpdateNotification(NotificationModel nm);

        /// <summary>
        /// Delete a notification
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int DeleteNotification(NotificationModel nm);

        /// <summary>
        /// Retrieve all the notifications
        /// </summary>
        /// <returns></returns>
        List<NotificationModel> FindAllNotifications();

    }
}
