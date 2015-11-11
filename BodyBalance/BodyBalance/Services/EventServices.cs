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
        private ConverterUtilities cu = new ConverterUtilities();

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

            return cu.ConvertEventToEventModel(ev);
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
                eventsList.Add(cu.ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        public List<UserModel> FindUsersOfEvent(string EventId)
        {
            List<UserModel> usersList = new List<UserModel>();
            IQueryable<USER1> query = db.Set<USER1>().Where(USER1 => USER1.EVENT.Any(EVENT => EVENT.EVENT_ID == EventId));

            foreach (USER1 u in query)
            {
                usersList.Add(cu.ConvertUserToUserModel(u));
            }

            return usersList;
        }

        public ContributorModel FindContributorOfEvent(string EventId)
        {
            EVENT ev = db.EVENT.Find(EventId);

            CONTRIBUTOR c = db.CONTRIBUTOR.Find(ev.EVENT_CONTRIBUTOR);

            return cu.ConvertContributorToContributorModel(c);
        }

        public ManagerModel FindManagerOfEvent(string EventId)
        {
            EVENT ev = db.EVENT.Find(EventId);

            MANAGER m = db.MANAGER.Find(ev.EVENT_MANAGER);

            return cu.ConvertManagerToManagerModel(m);
        }

        public int RegisterUserToEvent(string EventId, UserModel um)
        {
            int result = DaoUtilities.NO_CHANGES;

            EVENT ev = db.EVENT.Find(EventId);

            if (ev != null)
            {
                USER1 u = db.USER1.Find(um.UserId);
                if (u != null)
                {
                    u.EVENT.Add(ev);
                    ev.USER1.Add(u);
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
            }
            return result;
        }
    }
}