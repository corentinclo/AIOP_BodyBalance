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

            return ConvertManagerToManagerModel(m);
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
                managersList.Add(ConvertManagerToManagerModel(m));
            }

            return managersList;
        }

        private ManagerModel ConvertManagerToManagerModel(MANAGER m)
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
    }
}