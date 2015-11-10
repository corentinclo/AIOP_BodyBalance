using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Persistence;
using BodyBalance.Utilities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace BodyBalance.Services
{
    public class AccessoryServices : IAccessoryServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreateAccessory(AccessoryModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ACCESSORY a = db.ACCESSORY.Create();

            a.ACCESSORY_ID = am.AccessoryId;
            a.ACCESSORY_NAME = am.Name;
            a.ACCESSORY_QUANTITY = am.Quantity;

            db.ACCESSORY.Add(a);
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

        public AccessoryModel FindAccessoryById(string AccessoryId)
        {
            ACCESSORY a = db.ACCESSORY.Find(AccessoryId);

            return cu.ConvertAccesoryToAccessoryModel(a);
        }

        public int UpdateAccessory(AccessoryModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ACCESSORY a = db.ACCESSORY.Find(am.AccessoryId);

            if (a != null)
            {
                a.ACCESSORY_NAME = am.Name;
                a.ACCESSORY_QUANTITY = am.Quantity;

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

        public int DeleteAccessory(AccessoryModel am)
        {
            int result = DaoUtilities.NO_CHANGES;

            ACCESSORY a = db.ACCESSORY.Find(am.AccessoryId);

            if (a != null)
            {
                db.ACCESSORY.Remove(a);
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

        public List<AccessoryModel> FindAllAccessories()
        {
            List<AccessoryModel> accessoriesList = new List<AccessoryModel>();
            IQueryable<ACCESSORY> query = db.Set<ACCESSORY>();

            foreach (ACCESSORY a in query)
            {
                accessoriesList.Add(cu.ConvertAccesoryToAccessoryModel(a));
            }

            return accessoriesList;
        }
    }
}