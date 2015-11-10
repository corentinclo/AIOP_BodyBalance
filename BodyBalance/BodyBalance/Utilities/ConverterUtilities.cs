using BodyBalance.Models;
using BodyBalance.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Utilities
{
    public class ConverterUtilities
    {

        private Entities db = new Entities();

        public UserModel ConvertUserToUserModel(USER1 u)
        {
            UserModel um = new UserModel();

            if (u != null)
            {
                um.UserId = u.USER_ID;
                um.Password = u.USER_PASSWORD;
                um.FirstName = u.USER_FIRSTNAME;
                um.LastName = u.USER_LASTNAME;
                um.Adress1 = u.USER_ADR1;
                um.Adress2 = u.USER_ADR2;
                um.PC = u.USER_PC;
                um.Town = u.USER_TOWN;
                um.Phone = u.USER_PHONE;
                um.Mail = u.USER_MAIL;
            }
            else
                um = null;

            return um;
        }

        public AdminModel ConvertAdminToAdminModel(ADMIN a)
        {
            USER1 u = db.USER1.Find(a.ADMIN_ID);

            AdminModel am = new AdminModel();

            if (a != null && u != null)
            {
                am.UserId = u.USER_ID;
                am.Password = u.USER_PASSWORD;
                am.FirstName = u.USER_FIRSTNAME;
                am.LastName = u.USER_LASTNAME;
                am.Adress1 = u.USER_ADR1;
                am.Adress2 = u.USER_ADR2;
                am.PC = u.USER_PC;
                am.Town = u.USER_TOWN;
                am.Phone = u.USER_PHONE;
                am.Mail = u.USER_MAIL;
            }
            else
                am = null;

            return am;
        }

        public ContributorModel ConvertContributorToContributorModel(CONTRIBUTOR c)
        {
            USER1 u = db.USER1.Find(c.CONTRIBUTOR_ID);

            ContributorModel cm = new ContributorModel();

            if (c != null && u != null)
            {
                cm.UserId = u.USER_ID;
                cm.Password = u.USER_PASSWORD;
                cm.FirstName = u.USER_FIRSTNAME;
                cm.LastName = u.USER_LASTNAME;
                cm.Adress1 = u.USER_ADR1;
                cm.Adress2 = u.USER_ADR2;
                cm.PC = u.USER_PC;
                cm.Town = u.USER_TOWN;
                cm.Phone = u.USER_PHONE;
                cm.Mail = u.USER_MAIL;

                cm.ShortDesc = c.CONTRIBUTOR_SHORTDESC;
                cm.LongDesc = c.CONTRIBUTOR_LONGDESC;
            }
            else
                cm = null;

            return cm;
        }

        public ManagerModel ConvertManagerToManagerModel(MANAGER m)
        {
            USER1 u = db.USER1.Find(m.MANAGER_ID);

            ManagerModel mm = new ManagerModel();

            if (m != null && u != null)
            {
                mm.UserId = u.USER_ID;
                mm.Password = u.USER_PASSWORD;
                mm.FirstName = u.USER_FIRSTNAME;
                mm.LastName = u.USER_LASTNAME;
                mm.Adress1 = u.USER_ADR1;
                mm.Adress2 = u.USER_ADR2;
                mm.PC = u.USER_PC;
                mm.Town = u.USER_TOWN;
                mm.Phone = u.USER_PHONE;
                mm.Mail = u.USER_MAIL;
            }
            else
                mm = null;

            return mm;
        }

        public MemberModel ConvertMemberToMemberModel(MEMBER m)
        {
            USER1 u = db.USER1.Find(m.MEMBER_ID);

            MemberModel mm = new MemberModel();

            if (m != null && u != null)
            {
                mm.UserId = u.USER_ID;
                mm.Password = u.USER_PASSWORD;
                mm.FirstName = u.USER_FIRSTNAME;
                mm.LastName = u.USER_LASTNAME;
                mm.Adress1 = u.USER_ADR1;
                mm.Adress2 = u.USER_ADR2;
                mm.PC = u.USER_PC;
                mm.Town = u.USER_TOWN;
                mm.Phone = u.USER_PHONE;
                mm.Mail = u.USER_MAIL;

                mm.PayDate = m.MEMBER_PAYFEEDATE;
            }
            else
                mm = null;

            return mm;
        }

        public ActivityModel ConvertActivityToActivityModel(ACTIVITY a)
        {
            ActivityModel am = new ActivityModel();

            if (a != null)
            {
                am.ActivityId = a.ACTIVITY_ID;
                am.Name = a.ACTIVITY_NAME;
                am.ShortDesc = a.ACTIVITY_SHORTDESC;
                am.LongDesc = a.ACTIVITY_LONGDESC;
                am.ManagerId = a.ACTIVITY_MANAGER;
            }
            else
                am = null;

            return am;
        }

        public EventModel ConvertEventToEventModel(EVENT e)
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

        public RoomModel ConvertRoomToRoomModel(ROOM r)
        {
            RoomModel rm = new RoomModel();

            if (r != null)
            {
                rm.RoomId = r.ROOM_ID;
                rm.Name = r.ROOM_NAME;
                rm.Superficy = r.ROOM_SUPERFICY;
                rm.MaxNb = r.ROOM_MAXNBR;
            }
            else
                rm = null;

            return rm;
        }

        public AccessoryModel ConvertAccesoryToAccessoryModel(ACCESSORY a)
        {
            AccessoryModel am = new AccessoryModel();

            if (a != null)
            {
                am.AccessoryId = a.ACCESSORY_ID;
                am.Name = a.ACCESSORY_NAME;
                am.Quantity = a.ACCESSORY_QUANTITY;
            }
            else
                am = null;

            return am;
        }
    }
}