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
    public class EventServices : IEventServices
    {
        private Entities db = new Entities();

        public int CreateEvent(EventModel em)
        {
            int result = DaoUtilities.NO_CHANGES;

            EVENT ev = db.EVENT.Create();

            ev.EVENT_ID = em.EventId;
            ev.EVENT_NAME = em.name;
            ev.EVENT_DURATION = em.Duration;
            ev.EVENT_MAXNBR = em.MaxNb;
            ev.EVENT_PRICE = em.Price;
            ev.EVENT_ROOM = em.RoomId;
            ev.EVENT_ACTIVITY = em.ActivityId;
            ev.EVENT_CONTRIBUTOR = em.ContributorId;
            ev.EVENT_MANAGER = em.ManagerId;
            ev.EVENT_TYPE = em.Type;
            ev.EVENT_DATE = em.EventDate;

            db.EVENT.Add(ev);
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

        public EventModel FindEventById(string EventId)
        {
            EVENT ev = db.EVENT.Find(EventId);

            return ConvertEventToEventModel(ev);
        }

        public int UpdateEvent(EventModel em)
        {
            int result = DaoUtilities.NO_CHANGES;

            EVENT ev = db.EVENT.Find(em.EventId);

            if (ev != null)
            {
                ev.EVENT_NAME = em.name;
                ev.EVENT_DURATION = em.Duration;
                ev.EVENT_MAXNBR = em.MaxNb;
                ev.EVENT_PRICE = em.Price;
                ev.EVENT_ROOM = em.RoomId;
                ev.EVENT_ACTIVITY = em.ActivityId;
                ev.EVENT_CONTRIBUTOR = em.ContributorId;
                ev.EVENT_MANAGER = em.ManagerId;
                ev.EVENT_TYPE = em.Type;
                ev.EVENT_DATE = em.EventDate;

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


        public int DeleteEvent(EventModel em)
        {
            int result = DaoUtilities.NO_CHANGES;

            EVENT ev = db.EVENT.Find(em.EventId);

            if (ev != null)
            {
                db.EVENT.Remove(ev);
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


        public List<EventModel> FindAllEvents()
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>();

            foreach (EVENT e in query)
            {
                eventsList.Add(ConvertEventToEventModel(e));
            }

            return eventsList;
        }
        public List<EventModel> FindAllEventsOfActivity(string ActivityId)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_ACTIVITY == ActivityId);

            foreach (EVENT e in query)
            {
                eventsList.Add(ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        public List<EventModel> FindAllEventsOfContributor(string ContributorID)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_CONTRIBUTOR == ContributorID);

            foreach (EVENT e in query)
            {
                eventsList.Add(ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        public List<EventModel> FindAllEventsOfManager(string ManagerId)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_MANAGER == ManagerId);

            foreach (EVENT e in query)
            {
                eventsList.Add(ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        private EventModel ConvertEventToEventModel(EVENT e)
        {
            EventModel em = new EventModel();

            if (e != null)
            {
                em.EventId = e.EVENT_ID;
                em.name = e.EVENT_NAME;
                em.Duration = e.EVENT_DURATION;
                em.MaxNb = e.EVENT_MAXNBR;
                em.Price = e.EVENT_PRICE;
                em.RoomId = e.EVENT_ROOM;
                em.ActivityId = e.EVENT_ACTIVITY;
                em.ContributorId = e.EVENT_CONTRIBUTOR;
                em.ManagerId = e.EVENT_MANAGER;
                em.Type = e.EVENT_TYPE;
                em.EventDate = e.EVENT_DATE;
            }
            else
                em = null;

            return em;
        }
    }
}