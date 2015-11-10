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
    public class ManagerServices : IManagerServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreateManager(ManagerModel mm)
        {
            int result = DaoUtilities.NO_CHANGES;

            MANAGER m = db.MANAGER.Create();

            m.MANAGER_ID = mm.UserId;

            db.MANAGER.Add(m);
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

        public ManagerModel FindManagerById(string ManagerId)
        {
            MANAGER m = db.MANAGER.Find(ManagerId);

            return cu.ConvertManagerToManagerModel(m);
        }

        public int DeleteManager(ManagerModel mm)
        {
            int result = DaoUtilities.NO_CHANGES;

            MANAGER m = db.MANAGER.Find(mm.UserId);

            if (m != null)
            {
                db.MANAGER.Remove(m);
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

        public List<ManagerModel> FindAllManagers()
        {
            List<ManagerModel> managersList = new List<ManagerModel>();
            IQueryable<MANAGER> query = db.Set<MANAGER>();

            foreach (MANAGER m in query)
            {
                managersList.Add(cu.ConvertManagerToManagerModel(m));
            }

            return managersList;
        }

        public List<ActivityModel> FindAllActivitiesOfManager(string ManagerId)
        {
            List<ActivityModel> activitiesList = new List<ActivityModel>();
            IQueryable<ACTIVITY> query = db.Set<ACTIVITY>().Where(ACTIVITY => ACTIVITY.ACTIVITY_MANAGER == ManagerId);

            foreach (ACTIVITY a in query)
            {
                activitiesList.Add(cu.ConvertActivityToActivityModel(a));
            }

            return activitiesList;
        }

        public List<EventModel> FindAllEventsOfManager(string ManagerId)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_MANAGER == ManagerId);

            foreach (EVENT e in query)
            {
                eventsList.Add(cu.ConvertEventToEventModel(e));
            }

            return eventsList;
        }
    }
}