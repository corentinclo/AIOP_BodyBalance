using BodyBalance.Models;
using BodyBalance.Persistence;
using BodyBalance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Utilities
{
    public class ConverterUtilities
    {

        private Entities db = new Entities();

        /// <summary>
        /// Convert a user from the database to a UserModel
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
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

                var userServices = new UserServices();
                um.UserRoles = new RolesModel();
                um.UserRoles.IsAdmin = userServices.IsAdmin(um);
                um.UserRoles.IsContributor = userServices.IsContributor(um);
                um.UserRoles.IsManager = userServices.IsManager(um);
                um.UserRoles.IsMember = userServices.IsMember(um);
            }
            else
                um = null;

            return um;
        }

        /// <summary>
        /// Convert an admin from the database to an AdminModel
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a contributor from the database to a ContributorModel
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a manager from the database to a ManagerModel
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a member from the database to a MemberModel
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert an activity from the database to an ActivityModel
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a event from the database to a EventModel
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a room from the database to a RoomModel
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert an accessory from the database to an AccessoryModel
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a notification from the database to a NotificatiobModel
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public NotificationModel ConvertNotificationToNotificatiobModel(NOTIFICATION n)
        {
            NotificationModel nm = new NotificationModel();

            if (n != null)
            {
                nm.NotificationId = n.NOTIF_ID;
                nm.Title = n.NOTIF_NAME;
                nm.Message = n.NOTIF_MESSAGE;
                nm.NotifDate = n.NOTIF_DATE;
                nm.UserId = n.NOTIF_USERID;
            }
            else
                nm = null;

            return nm;
        }

        /// <summary>
        /// Convert a product from the database to a ProductModel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ProductModel ConvertProductToProductModel(PRODUCT p)
        {
            ProductModel pm = new ProductModel();

            if (p != null)
            {
                pm.ProductId = p.PRODUCT_ID;
                pm.Name = p.PRODUCT_NAME;
                pm.Description = p.PRODUCT_DESCRIPTION;
                pm.AvailableQuantity = p.PRODUCT_AVAILABLEQUANTITY;
                pm.MemberReduction = p.PRODUCT_MEMBERREDUCTION;
                pm.CategoryId = p.PRODUCT_CAT;
                pm.UserId = p.PRODUCT_USERID;
            }
            else
                pm = null;

            return pm;
        }

        /// <summary>
        /// Convert a category from the database to a CategoryModel
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public CategoryModel ConvertCategoryToCategoryModel(CATEGORY c)
        {
            CategoryModel cm = new CategoryModel();

            if (c != null)
            {
                cm.CategoryId = c.CAT_ID;
                cm.Name = c.CAT_NAME;
                cm.Description = c.CAT_DESCR;
                cm.ValidationDate = c.CAT_VALIDATIONDATE;
                cm.ParentId = c.CAT_PARENT;
            }
            else
                cm = null;

            return cm;
        }

        /// <summary>
        /// Convert a price from the database to a PriceModel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public PriceModel ConvertPriceToPriceModel(PRICE p)
        {
            PriceModel pm = new PriceModel();

            if (p != null)
            {
                pm.ProductId = p.PRODUCT_ID;
                pm.DatePrice = p.DATE_PRICE;
                pm.ProductPrice = p.PRODUCT_PRICE;
            }
            else
               pm = null;

            return pm;
        }

        /// <summary>
        /// Convert a purchase from the database to a PurchaseModel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public PurchaseModel ConvertPurchaseToPurchaseModel(PURCHASE p)
        {
            PurchaseModel pm = new PurchaseModel();

            if (p != null)
            {
                pm.PurchaseId = p.PURCHASE_ID;
                pm.UserId = p.PURCHASE_USERID;
                pm.PurchaseDate = p.PURCHASE_DATE;
                pm.ShipDate = p.PURCHASE_SHIPDATE;
                pm.TotalPrice = p.PURCHASE_TOTALPRICE;
            }
            else
                pm = null;

            return pm;
        }

        /// <summary>
        /// Convert a purchaseContains from the database to a PurchaseLineModel
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        public PurchaseLineModel ConvertPurchaseContainsToPurchaseLineModel(PURCHASECONTAINS pc)
        {
            PurchaseLineModel plm = new PurchaseLineModel();

            if (pc != null)
            {
                plm.PurchaseId = pc.PURCHASE_ID;
                plm.ProductId = pc.PRODUCT_ID;
                plm.Quantity = pc.PRODUCTQUANTITY;
                plm.ValidationDate = pc.VALIDATIONDATE;
            }
            else
                plm = null;

            return plm;
        }

        /// <summary>
        /// Convert a basket from the database to a BasketModel
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public BasketModel ConvertBasketToBasketModel(HASINBASKET b)
        {
            BasketModel bm = new BasketModel();

            if (b != null)
            {
                bm.UserId = b.USER_ID;
                bm.ProductId = b.PRODUCT_ID;
                bm.Quantity = b.QUANTITY;
            }
            else
                bm = null;

            return bm;
        }
    }
}