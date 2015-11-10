using BodyBalance.Models;
using BodyBalance.Persistence;
using BodyBalance.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace BodyBalance.Services
{
    public class ActivityServices : IActivityServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreateActivity(ActivityModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ACTIVITY a = db.ACTIVITY.Create();

            a.ACTIVITY_ID = am.ActivityId;
            a.ACTIVITY_NAME = am.Name;
            a.ACTIVITY_SHORTDESC = am.ShortDesc;
            a.ACTIVITY_LONGDESC = am.LongDesc;
            a.ACTIVITY_MANAGER = am.ManagerId;

            db.ACTIVITY.Add(a);
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

        public ActivityModel FindActivityById(string ActivityId)
        {
            ACTIVITY a = db.ACTIVITY.Find(ActivityId);

            return cu.ConvertActivityToActivityModel(a);
        }

        public int UpdateActivity(ActivityModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ACTIVITY a = db.ACTIVITY.Find(am.ActivityId);

            if(a != null)
            {
                a.ACTIVITY_NAME = am.Name;
                a.ACTIVITY_SHORTDESC = am.ShortDesc;
                a.ACTIVITY_LONGDESC = am.LongDesc;
                a.ACTIVITY_MANAGER = am.ManagerId;

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

        public int DeleteActivity(ActivityModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ACTIVITY a = db.ACTIVITY.Find(am.ActivityId);

            if(a != null)
            {
                db.ACTIVITY.Remove(a);
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

        public List<ActivityModel> FindAllActivities()
        {
            List<ActivityModel> activitiesList = new List<ActivityModel>();
            IQueryable<ACTIVITY> query = db.Set<ACTIVITY>();

            foreach (ACTIVITY a in query)
            {
                activitiesList.Add(cu.ConvertActivityToActivityModel(a));
            }

            return activitiesList;
        }

        public List<EventModel> FindAllEventsOfActivity(string ActivityId)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_ACTIVITY == ActivityId);

            foreach (EVENT e in query)
            {
                eventsList.Add(cu.ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        
    }
}