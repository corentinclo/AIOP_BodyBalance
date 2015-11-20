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

        public UserModel FindOneUserOfEvent(string EventId, string UserId)
        {
            List<UserModel> usersList = new List<UserModel>();
            USER1 u = db.Set<USER1>().Where(USER1 => USER1.USER_ID == UserId && USER1.EVENT.Any(EVENT => EVENT.EVENT_ID == EventId)).FirstOrDefault();

            UserModel um = cu.ConvertUserToUserModel(u);
            return um;
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
                    ev.EVENT_MAXNBR = ev.EVENT_MAXNBR - 1;
                    try
                    {
                        int saveResult = db.SaveChanges();

                        if (saveResult >= 1)
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

        public int RemoveUserOfEvent(string EventId, UserModel um)
        {
            int result = DaoUtilities.NO_CHANGES;

            EVENT ev = db.EVENT.Find(EventId);

            if (ev != null)
            {
                USER1 u = db.USER1.Find(um.UserId);
                if (u != null)
                {
                    u.EVENT.Remove(ev);
                    ev.USER1.Remove(u);
                    ev.EVENT_MAXNBR = ev.EVENT_MAXNBR + 1;
                    try
                    {
                        int saveResult = db.SaveChanges();

                        if (saveResult >= 1)
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

        public int CreatePunctualEvent(PunctualEventModel em)
        {
            int result = CreateEvent(em);

            if (result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                PUNCTUAL_EVENT pe = db.PUNCTUAL_EVENT.Create();

                pe.PE_ID = em.EventId;
                pe.PE_DATE = em.EventDate;

                db.PUNCTUAL_EVENT.Add(pe);
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

        public int CreateRepetitiveEvent(RepetitiveEventModel em)
        {
            int result = CreateEvent(em);

            if (result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                REPETITIVE_EVENT re = db.REPETITIVE_EVENT.Create();

                re.RE_ID = em.EventId;
                re.RE_DATE = em.EventDate;

                db.REPETITIVE_EVENT.Add(re);
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

        public int AddRepetitiveEventOccurrence(RepetitiveEventModel em)
        {
            int result = DaoUtilities.NO_CHANGES;
            REPETITIVE_EVENT re = db.REPETITIVE_EVENT.Create();

            re.RE_ID = em.EventId;
            re.RE_DATE = em.EventDate;

            db.REPETITIVE_EVENT.Add(re);
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

        public PunctualEventModel FindPunctualEventById(string EventId)
        {
            PUNCTUAL_EVENT pe = db.PUNCTUAL_EVENT.Find(EventId);

            PunctualEventModel pem = null;

            if (pe != null)
            {
                EventModel em = FindEventById(EventId);
                pem = new PunctualEventModel();
                pem = ((PunctualEventModel) em);
            }

            return pem;   
        }

        public List<RepetitiveEventModel> FindRepetitiveEventsById(string EventId)
        {
            EventModel em = FindEventById(EventId);

            List<RepetitiveEventModel> repetitiveEventsList = new List<RepetitiveEventModel>();
            if (em != null)
            {
                IQueryable<REPETITIVE_EVENT> query = db.Set<REPETITIVE_EVENT>().Where(REPETITIVE_EVENT => REPETITIVE_EVENT.RE_ID == em.EventId);

                foreach (REPETITIVE_EVENT re in query)
                {
                    em.EventDate = re.RE_DATE;
                    repetitiveEventsList.Add((RepetitiveEventModel) em);
                }
            }
            return repetitiveEventsList;
        }

        public RepetitiveEventModel FindRepetitiveEventByIdAndDate(string EventId, DateTime EventDate)
        {
            REPETITIVE_EVENT re = db.REPETITIVE_EVENT.Find(EventId, EventDate);

            RepetitiveEventModel rem = null;

            if (re != null)
            {
                EventModel em = FindEventById(EventId);
                rem = new RepetitiveEventModel();
                rem = ((RepetitiveEventModel)em);
                rem.EventDate = EventDate;
            }

            return rem;
        }

        public int UpdatePunctualEvent(PunctualEventModel em)
        {
            int result = UpdateEvent(em);

            if (result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                PUNCTUAL_EVENT pe = db.PUNCTUAL_EVENT.Find(em.EventId);

                if(pe != null)
                {
                    pe.PE_DATE = em.EventDate;

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

        public int UpdateRepetitiveEvent(RepetitiveEventModel em)
        {
            int result = UpdateEvent(em);

            if (result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                REPETITIVE_EVENT re = db.REPETITIVE_EVENT.Find(em.EventId, em.EventDate);

                if(re != null)
                {
                    re.RE_DATE = em.EventDate;

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

        public int DeletePunctualEvent(PunctualEventModel em)
        {
            int result = DeleteEvent(em);

            if (result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                PUNCTUAL_EVENT pe = db.PUNCTUAL_EVENT.Find(em.EventId);

                if(pe != null)
                {
                    db.PUNCTUAL_EVENT.Remove(pe);

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

        public int DeleteRepetitiveEvent(RepetitiveEventModel em)
        {
            int result = DaoUtilities.NO_CHANGES;

            REPETITIVE_EVENT re = db.REPETITIVE_EVENT.Find(em.EventId, em.EventDate);

            if (re != null)
            {
                db.REPETITIVE_EVENT.Remove(re);
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

        public int DeleteRepetitiveEvents(EventModel em)
        {
            int result = DeleteEvent(em);

            if (result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                IQueryable<REPETITIVE_EVENT> query = db.Set<REPETITIVE_EVENT>().Where(REPETITIVE_EVENT => REPETITIVE_EVENT.RE_ID == em.EventId);

                foreach (REPETITIVE_EVENT re in query)
                {
                    db.REPETITIVE_EVENT.Remove(re);
                }

                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult != 0)
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
        public List<PunctualEventModel> FindAllPunctualEvents()
        {
            List<PunctualEventModel> punctualEventsList = new List<PunctualEventModel>();
            IQueryable<PUNCTUAL_EVENT> query = db.Set<PUNCTUAL_EVENT>();

            foreach (PUNCTUAL_EVENT pe in query)
            {
                punctualEventsList.Add((PunctualEventModel) FindEventById(pe.PE_ID));
            }

            return punctualEventsList;
        }

        public List<RepetitiveEventModel> FindAllRepetitiveEvents()
        {
            List<RepetitiveEventModel> repetitiveEventsList = new List<RepetitiveEventModel>();
            IQueryable<REPETITIVE_EVENT> query = db.Set<REPETITIVE_EVENT>();

            foreach (REPETITIVE_EVENT re in query)
            {
                RepetitiveEventModel rem = (RepetitiveEventModel) FindEventById(re.RE_ID);
                rem.EventDate = re.RE_DATE;
                repetitiveEventsList.Add(rem);
            }

            return repetitiveEventsList;
        }

        public List<string> FindAllTypes()
        {
            List<string> result = new List<string>();
            IQueryable<EVENTTYPE> query = db.Set<EVENTTYPE>();

            foreach (EVENTTYPE t in query)
                result.Add(t.EVENTTYPE_ID);

            return result;
        }
    }
}