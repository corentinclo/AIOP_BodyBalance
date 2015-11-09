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
    public class ContributorServices : IContributorServices
    {
        private Entities db = new Entities();

        public int CreateContributor(ContributorModel cm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CONTRIBUTOR c = db.CONTRIBUTOR.Create();

            c.CONTRIBUTOR_ID = cm.UserId;
            c.CONTRIBUTOR_SHORTDESC = cm.ShortDesc;
            c.CONTRIBUTOR_LONGDESC = cm.LongDesc;

            db.CONTRIBUTOR.Add(c);
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

        public ContributorModel FindContributorById(string ContributorId)
        {
            CONTRIBUTOR c = db.CONTRIBUTOR.Find(ContributorId);

            return ConvertContributorToContributorModel(c);
        }

        public int UpdateContributor(ContributorModel cm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CONTRIBUTOR c = db.CONTRIBUTOR.Find(cm.UserId);

            if (c != null)
            {
                c.CONTRIBUTOR_SHORTDESC = cm.ShortDesc;
                c.CONTRIBUTOR_LONGDESC = cm.LongDesc;

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

        public int DeleteContributor(ContributorModel cm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CONTRIBUTOR c = db.CONTRIBUTOR.Find(cm.UserId);

            if (c != null)
            {
                db.CONTRIBUTOR.Remove(c);
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

        public List<ContributorModel> FindAllContributors()
        {
            List<ContributorModel> contributorsList = new List<ContributorModel>();
            IQueryable<CONTRIBUTOR> query = db.Set<CONTRIBUTOR>();

            foreach (CONTRIBUTOR c in query)
            {
                contributorsList.Add(ConvertContributorToContributorModel(c));
            }

            return contributorsList;
        }

        private ContributorModel ConvertContributorToContributorModel(CONTRIBUTOR c)
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
    }
}