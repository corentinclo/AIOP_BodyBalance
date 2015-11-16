using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Persistence;
using System.Security.Cryptography;
using System.Text;
using BodyBalance.Utilities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace BodyBalance.Services
{
    public class UserServices : IUserServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreateUser(UserModel um)
        {
            int result = DaoUtilities.NO_CHANGES;

            USER1 u = db.USER1.Create();

            u.USER_ID = um.UserId;
            u.USER_PASSWORD = hashSHA512(um.Password);
            u.USER_FIRSTNAME = um.FirstName;
            u.USER_LASTNAME = um.LastName;
            u.USER_ADR1 = um.Adress1;
            u.USER_ADR2 = um.Adress2;
            u.USER_PC = um.PC;
            u.USER_TOWN = um.Town;
            u.USER_PHONE = um.Phone;
            u.USER_MAIL = um.Mail;

            db.USER1.Add(u);
            try {
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

        public UserModel FindUserById(string UserId)
        {
            USER1 u = db.USER1.Find(UserId);

            return cu.ConvertUserToUserModel(u);
        }

        public UserModel FindUserByIdAndPassword(string id, string pwd)
        {
            pwd = hashSHA512(pwd);
            USER1 u = ((USER1) db.USER1.Where(USER1 => USER1.USER_ID == id && USER1.USER_PASSWORD == pwd).FirstOrDefault());

            UserModel um = cu.ConvertUserToUserModel(u);

            if (um != null)
            {
                um.UserRoles = new RolesModel();
                um.UserRoles.IsAdmin = IsAdmin(um);
                um.UserRoles.IsContributor = IsContributor(um);
                um.UserRoles.IsManager = IsManager(um);
                um.UserRoles.IsMember = IsMember(um);
            }
            return um;
        }

        public int UpdateUser(UserModel um)
        {
            int result = DaoUtilities.NO_CHANGES;

            USER1 u = db.USER1.Find(um.UserId);

            if (u != null)
            {
                u.USER_PASSWORD = hashSHA512(um.Password);
                u.USER_FIRSTNAME = um.FirstName;
                u.USER_LASTNAME = um.LastName;
                u.USER_ADR1 = um.Adress1;
                u.USER_ADR2 = um.Adress2;
                u.USER_PC = um.PC;
                u.USER_TOWN = um.Town;
                u.USER_PHONE = um.Phone;
                u.USER_MAIL = um.Mail;

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

        public int DeleteUser(UserModel um)
        {
            int result = DaoUtilities.NO_CHANGES;

            USER1 u = db.USER1.Find(um.UserId);

            if (u != null)
            {
                db.USER1.Remove(u);
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

        public List<UserModel> FindAllUsers()
        {
            List<UserModel> usersList = new List<UserModel>();
            IQueryable<USER1> query = db.Set<USER1>();

            foreach(USER1 u in query)
            {
                usersList.Add(cu.ConvertUserToUserModel(u));
            }

            return usersList;
        }

        public bool IsAdmin(UserModel um)
        {
            if (db.ADMIN.Find(um.UserId) != null)
                return true;

            return false;
        }

        public bool IsContributor(UserModel um)
        {
            if (db.CONTRIBUTOR.Find(um.UserId) != null)
                return true;

            return false;
        }

        public bool IsManager(UserModel um)
        {
            if (db.MANAGER.Find(um.UserId) != null)
                return true;

            return false;
        }

        public bool IsMember(UserModel um)
        {
            if (db.MEMBER.Find(um.UserId) != null)
                return true;

            return false;
        }

        public List<ProductModel> FindAllProductsOfUser(string UserId)
        {
            List<ProductModel> productsList = new List<ProductModel>();
            IQueryable<PRODUCT> query = db.Set<PRODUCT>().Where(PRODUCT => PRODUCT.PRODUCT_USERID == UserId);

            foreach (PRODUCT p in query)
            {
                productsList.Add(cu.ConvertProductToProductModel(p));
            }

            return productsList;
        }

        public List<NotificationModel> FindAllNotificationssOfUser(string UserId)
        {
            List<NotificationModel> notificationsList = new List<NotificationModel>();
            IQueryable<NOTIFICATION> query = db.Set<NOTIFICATION>().Where(NOTIFICATION => NOTIFICATION.NOTIF_USERID == UserId);

            foreach (NOTIFICATION n in query)
            {
                notificationsList.Add(cu.ConvertNotificationToNotificatiobModel(n));
            }

            return notificationsList;
        }

        public List<PurchaseModel> FindAllPurchasesOfUser(string UserId)
        {
            List<PurchaseModel> purchasesList = new List<PurchaseModel>();
            IQueryable<PURCHASE> query = db.Set<PURCHASE>().Where(PURCHASE => PURCHASE.PURCHASE_USERID == UserId);

            foreach (PURCHASE p in query)
            {
                purchasesList.Add(cu.ConvertPurchaseToPurchaseModel(p));
            }

            return purchasesList;
        }

        public List<BasketModel> FindBasketOfUser(string UserId)
        {
            List<BasketModel> basketsList = new List<BasketModel>();
            IQueryable<HASINBASKET> query = db.Set<HASINBASKET>().Where(HASINBASKET => HASINBASKET.USER_ID == UserId);

            foreach (HASINBASKET b in query)
            {
                basketsList.Add(cu.ConvertBasketToBasketModel(b));
            }

            return basketsList;
        }

        public int DeleteUserBasket(string UserId)
        {
            int result = DaoUtilities.NO_CHANGES;

            IQueryable<HASINBASKET> query = db.Set<HASINBASKET>().Where(HASINBASKET => HASINBASKET.USER_ID == UserId);

            foreach (HASINBASKET b in query)
            {
                db.HASINBASKET.Remove(b);
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

            return result;
        }

        public int CreateUserPurchase(string UserId)
        {
            int result = DaoUtilities.NO_CHANGES;

            IProductServices prs = new ProductServices();
            IPurchaseServices pus = new PurchaseServices();
            IPurchaseLineServices pls = new PurchaseLineServices();

            List<BasketModel> bm = FindBasketOfUser(UserId);

            PurchaseModel pm = new PurchaseModel();
            pm.PurchaseId = Guid.NewGuid().ToString();
            pm.PurchaseDate = DateTime.Today;
            pm.UserId = UserId;
            pm.TotalPrice = 0;

            List<PurchaseLineModel> purchaseLines = new List<PurchaseLineModel>();

            foreach (BasketModel bl in bm)
            {
                ProductModel prm = prs.FindProductWithId(bl.ProductId);
                pm.TotalPrice += prm.Price;
                PurchaseLineModel plm = new PurchaseLineModel();
                plm.PurchaseId = pm.PurchaseId;
                plm.ProductId = bl.ProductId;
                plm.Quantity = bl.Quantity;
                purchaseLines.Add(plm);
            }

            result = pus.CreatePurchase(pm);
            if(result == DaoUtilities.SAVE_SUCCESSFUL)
            {
                foreach(PurchaseLineModel plm in purchaseLines)
                {
                    result = pls.CreatePurchaseLine(plm);
                    if (result != DaoUtilities.SAVE_SUCCESSFUL)
                        break;
                }
            }

            return result;
        }

        public List<EventModel> FindAllEventsOfUser(string UserId)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.USER1.Any(USER1 => USER1.USER_ID == UserId) );

            foreach (EVENT e in query)
            {
                eventsList.Add(cu.ConvertEventToEventModel(e));
            }

            return eventsList;
        }

        private string hashSHA512(string unhashedValue)
        {
            SHA512 shaM = new SHA512Managed();
            byte[] hash = shaM.ComputeHash(Encoding.ASCII.GetBytes(unhashedValue));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}