﻿using BodyBalance.Persistence;
using BodyBalance.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace BodyBalance.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreatePurchase(PurchaseModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            PURCHASE p = db.PURCHASE.Create();

            p.PURCHASE_ID = pm.PurchaseId;
            p.PURCHASE_USERID = pm.UserId;
            p.PURCHASE_DATE = pm.PurchaseDate;
            p.PURCHASE_SHIPDATE = pm.ShipDate;
            p.PURCHASE_TOTALPRICE = pm.TotalPrice;

            db.PURCHASE.Add(p);
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

        public PurchaseModel FindPurchaseWithId(string PurchaseId)
        {
            PURCHASE p = db.PURCHASE.Find(PurchaseId);

            return cu.ConvertPurchaseToPurchaseModel(p);
        }

        public int UpdatePurchase(PurchaseModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            PURCHASE p = db.PURCHASE.Find(pm.PurchaseId);
               
            if(p != null)
            {
                p.PURCHASE_USERID = pm.UserId;
                p.PURCHASE_DATE = pm.PurchaseDate;
                p.PURCHASE_SHIPDATE = pm.ShipDate;
                p.PURCHASE_TOTALPRICE = pm.TotalPrice;

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

        public int DeletePurchase(PurchaseModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            PURCHASE p = db.PURCHASE.Find(pm.PurchaseId);

            if (p != null)
            {
                db.PURCHASE.Remove(p);

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

        public List<PurchaseModel> FindAllPurchases()
        {
            List<PurchaseModel> purchasesList = new List<PurchaseModel>();
            IQueryable<PURCHASE> query = db.Set<PURCHASE>();

            foreach (PURCHASE p in query)
            {
                purchasesList.Add(cu.ConvertPurchaseToPurchaseModel(p));
            }

            return purchasesList;
        }

        public List<PurchaseLineModel> FindAllLinesOfPurchase(string PurchaseId)
        {
            List<PurchaseLineModel> purchaseLinesList = new List<PurchaseLineModel>();
            IQueryable<PURCHASECONTAINS> query = db.Set<PURCHASECONTAINS>().Where(PURCHASECONTAINS => PURCHASECONTAINS.PURCHASE_ID== PurchaseId);

            foreach (PURCHASECONTAINS pc in query)
            {
                purchaseLinesList.Add(cu.ConvertPurchaseContainsToPurchaseLineModel(pc));
            }

            return purchaseLinesList;
        }
    }
}