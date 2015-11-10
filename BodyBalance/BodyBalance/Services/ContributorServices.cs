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
        private ConverterUtilities cu = new ConverterUtilities();

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

            return cu.ConvertContributorToContributorModel(c);
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
                contributorsList.Add(cu.ConvertContributorToContributorModel(c));
            }

            return contributorsList;
        }

        public List<EventModel> FindAllEventsOfContributor(string ContributorID)
        {
            List<EventModel> eventsList = new List<EventModel>();
            IQueryable<EVENT> query = db.Set<EVENT>().Where(EVENT => EVENT.EVENT_CONTRIBUTOR == ContributorID);

            foreach (EVENT e in query)
            {
                eventsList.Add(cu.ConvertEventToEventModel(e));
            }

            return eventsList;
        }
    }
}