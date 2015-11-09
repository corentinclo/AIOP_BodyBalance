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

        public int CreateUser(UserModel um)
        {
            int result;

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
                result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
            }
            catch (DbUpdateException e)
            {
                result = DaoUtilities.UPDATE_EXCEPTION;
            }
            catch (DbEntityValidationException e)
            {
                result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
            }
            catch (NotSupportedException e)
            {
                result = DaoUtilities.UNSUPPORTED_EXCEPTION;
            }
            catch (ObjectDisposedException e)
            {
                result = DaoUtilities.DISPOSED_EXCEPTION;
            }
            catch (InvalidOperationException e)
            {
                result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
            }

            result = DaoUtilities.NO_CHANGES;
            return result;
        }

        public UserModel FindUserById(String id)
        {
            USER1 u = db.USER1.Find(id);

            return ConvertUserToUserModel(u);
        }

        public UserModel FindUserByIdAndPassword(String id, String pwd)
        {
            pwd = hashSHA512(pwd);
            USER1 u = ((USER1) db.USER1.Where(USER1 => USER1.USER_ID == id && USER1.USER_PASSWORD == pwd).FirstOrDefault());

            return ConvertUserToUserModel(u);
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
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }

                result = DaoUtilities.NO_CHANGES;
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
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }

                result = DaoUtilities.NO_CHANGES;
            }
            return result;
        }

        public List<UserModel> FindAllUsers()
        {
            List<UserModel> usersList = new List<UserModel>();
            IQueryable<USER1> query = db.Set<USER1>();

            foreach(USER1 u in query)
            {
                usersList.Add(ConvertUserToUserModel(u));
            }

            return usersList;
        }

        public Boolean IsAdmin(UserModel um)
        {
            if (db.ADMIN.Find(um.UserId) != null)
                return true;

            return false;
        }

        public Boolean IsContributor(UserModel um)
        {
            if (db.CONTRIBUTOR.Find(um.UserId) != null)
                return true;

            return false;
        }

        public Boolean IsManager(UserModel um)
        {
            if (db.MANAGER.Find(um.UserId) != null)
                return true;

            return false;
        }

        public Boolean IsMember(UserModel um)
        {
            if (db.MEMBER.Find(um.UserId) != null)
                return true;

            return false;
        }

        private UserModel ConvertUserToUserModel(USER1 u)
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