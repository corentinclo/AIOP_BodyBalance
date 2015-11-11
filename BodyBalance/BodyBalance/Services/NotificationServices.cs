using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Persistence;
using BodyBalance.Utilities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace BodyBalance.Services
{
    public class NotificationServices : INotificationServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreateNotification(NotificationModel nm)
        {
            int result = DaoUtilities.NO_CHANGES;

            NOTIFICATION n = db.NOTIFICATION.Create();

            n.NOTIF_ID = nm.NotificationId;
            n.NOTIF_NAME = nm.Title;
            n.NOTIF_MESSAGE = nm.Message;
            n.NOTIF_DATE = nm.NotifDate;
            n.NOTIF_USERID = nm.UserId;

            db.NOTIFICATION.Add(n);
            try
            {
                int saveResult = db.SaveChanges();

                if (saveResult == 1)
                    result = DaoUtilities.SAVE_SUCCESSFUL;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.UPDATE_EXCEPTION;
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.UNSUPPORTED_EXCEPTION;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.DISPOSED_EXCEPTION;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
            }
            return result;
        }

        public NotificationModel FindNotificationWithId(string NotificationId)
        {
            NOTIFICATION n = db.NOTIFICATION.Find(NotificationId);

            return cu.ConvertNotificationToNotificatiobModel(n);
        }

        public int UpdateNotification(NotificationModel nm)
        {
            int result = DaoUtilities.NO_CHANGES;

            NOTIFICATION n = db.NOTIFICATION.Find(nm.NotificationId);

            if (n != null)
            {
                n.NOTIF_NAME = nm.Title;
                n.NOTIF_MESSAGE = nm.Message;
                n.NOTIF_DATE = nm.NotifDate;
                n.NOTIF_USERID = nm.UserId;

                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult == 1)
                        result = DaoUtilities.SAVE_SUCCESSFUL;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }
            }
            return result;
        }

        public int DeleteNotification(NotificationModel nm)
        {
            int result = DaoUtilities.NO_CHANGES;

            NOTIFICATION n = db.NOTIFICATION.Find(nm.NotificationId);

            if (n != null)
            {
                db.NOTIFICATION.Remove(n);
                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult == 1)
                        result = DaoUtilities.SAVE_SUCCESSFUL;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }
            }
            return result;
        }

        public List<NotificationModel> FindAllNotifications()
        {
            List<NotificationModel> notificationsList = new List<NotificationModel>();
            IQueryable<NOTIFICATION> query = db.Set<NOTIFICATION>();

            foreach (NOTIFICATION n in query)
            {
                notificationsList.Add(cu.ConvertNotificationToNotificatiobModel(n));
            }

            return notificationsList;
        }
    }
}