using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Utilities;
using BodyBalance.Persistence;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace BodyBalance.Services
{
    public class RoomServices : IRoomServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();
        private IAccessoryServices acs;

        public int CreateRoom(RoomModel rm)
        {
            int result = DaoUtilities.NO_CHANGES;

            ROOM r = db.ROOM.Create();

            r.ROOM_ID = rm.RoomId;
            r.ROOM_NAME = rm.Name;
            r.ROOM_SUPERFICY = rm.Superficy;
            r.ROOM_MAXNBR = rm.MaxNb;

            db.ROOM.Add(r);
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

        public RoomModel FindRoomById(string RoomId)
        {
            ROOM r = db.ROOM.Find(RoomId);

            return cu.ConvertRoomToRoomModel(r);
        }

        public int UpdateRoom(RoomModel rm)
        {
            int result = DaoUtilities.NO_CHANGES;

            ROOM r = db.ROOM.Find(rm.RoomId);

            if (r != null)
            {
                r.ROOM_NAME = rm.Name;
                r.ROOM_SUPERFICY = rm.Superficy;
                r.ROOM_MAXNBR = rm.MaxNb;

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

        public int DeleteRoom(RoomModel rm)
        {
            int result = DaoUtilities.NO_CHANGES;

            ROOM r = db.ROOM.Find(rm.RoomId);

            if (r != null)
            {
                db.ROOM.Remove(r);
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

        public List<RoomModel> FindAllRooms()
        {
            List<RoomModel> roomsList = new List<RoomModel>();
            IQueryable<ROOM> query = db.Set<ROOM>();

            foreach (ROOM r in query)
            {
                roomsList.Add(cu.ConvertRoomToRoomModel(r));
            }

            return roomsList;
        }

        public List<EventModel> FindAllEventsOfRoom(string RoomId)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_ROOM == RoomId);

            foreach (EVENT e in query)
            {
                eventsList.Add(cu.ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        public List<AccessoryModel> FindAllAccessoriesOfRoom(string RoomId)
        {
            List<AccessoryModel> accessoriesList = new List<AccessoryModel>();
            IQueryable<ACCESSORY> query = db.Set<ACCESSORY>().Where(ACCESSORY => ACCESSORY.CONTAINSACCESSORY.Any(CONTAINSACCESSORY => CONTAINSACCESSORY.ROOM_ID == RoomId));

            foreach (ACCESSORY a in query)
            {
                accessoriesList.Add(cu.ConvertAccesoryToAccessoryModel(a));
            }

            return accessoriesList;
        }

        public int AddAccessoryToRoom(string RoomId, AccessoryModel am, Nullable<decimal> quantity)
        {
            int result = DaoUtilities.NO_CHANGES;

            ROOM r = db.ROOM.Find(RoomId);

            if (r != null)
            {
                ACCESSORY a = db.ACCESSORY.Find(am.AccessoryId);
                if (a == null)
                {
                    acs = new AccessoryServices();
                    int creationResult = acs.CreateAccessory(am);
                    if(creationResult == DaoUtilities.SAVE_SUCCESSFUL)
                    {
                        CONTAINSACCESSORY ca = db.CONTAINSACCESSORY.Create();
                        ca.ROOM_ID = RoomId;
                        ca.ACCESSORY_ID = am.AccessoryId;
                        ca.CONTAINS_QUANTITY = quantity;
                        r.CONTAINSACCESSORY.Add(ca);
                        a = db.ACCESSORY.Find(am.AccessoryId);
                        a.CONTAINSACCESSORY.Add(ca);
                    }
                }
                else
                {
                    CONTAINSACCESSORY ca = db.CONTAINSACCESSORY.Create();
                    ca.ROOM_ID = RoomId;
                    ca.ACCESSORY_ID = a.ACCESSORY_ID;
                    ca.CONTAINS_QUANTITY = quantity;
                    r.CONTAINSACCESSORY.Add(ca);
                    a.CONTAINSACCESSORY.Add(ca);
                }
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
    }
}