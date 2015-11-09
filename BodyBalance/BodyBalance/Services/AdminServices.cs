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
    public class AdminServices : IAdminServices
    {
        private Entities db = new Entities();

        public int CreateAdmin(AdminModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ADMIN a = db.ADMIN.Create();

            a.ADMIN_ID = am.UserId;

            db.ADMIN.Add(a);
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

        public AdminModel FindAdminById(string AdminId)
        {
            ADMIN a = db.ADMIN.Find(AdminId);

            return ConvertAdminToAdminModel(a);
        }

        public int DeleteAdmin(AdminModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ADMIN a = db.ADMIN.Find(am.UserId);

            if (a != null)
            {
                db.ADMIN.Remove(a);
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

        public List<AdminModel> FindAllAdmins()
        {
            List<AdminModel> adminsList = new List<AdminModel>();
            IQueryable<ADMIN> query = db.Set<ADMIN>();

            foreach (ADMIN m in query)
            {
                adminsList.Add(ConvertAdminToAdminModel(m));
            }

            return adminsList;
        }

        private AdminModel ConvertAdminToAdminModel(ADMIN a)
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
    }
}