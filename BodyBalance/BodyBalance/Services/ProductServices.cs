using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Utilities;
using BodyBalance.Persistence;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace BodyBalance.Services
{
    public class ProductServices : IProductServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();

        public int CreateProduct(ProductModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            PRODUCT p = db.PRODUCT.Create();

            p.PRODUCT_ID = pm.ProductId;
            p.PRODUCT_NAME = pm.Name;
            p.PRODUCT_DESCRIPTION = pm.Description;
            p.PRODUCT_AVAILABLEQUANTITY = pm.AvailableQuantity;
            p.PRODUCT_MEMBERREDUCTION = pm.MemberReduction;
            p.PRODUCT_CAT = pm.CategoryId;
            p.PRODUCT_USERID = pm.UserId;
            p.PRODUCT_PRICE = pm.Price;

            db.PRODUCT.Add(p);
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

        public ProductModel FindProductWithId(string ProductId)
        {
            PRODUCT p = db.PRODUCT.Find(ProductId);

            return cu.ConvertProductToProductModel(p);
        }

        public int UpdateProduct(ProductModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            PRODUCT p = db.PRODUCT.Find(pm.ProductId);

            if (p != null)
            {
                p.PRODUCT_ID = pm.ProductId;
                p.PRODUCT_NAME = pm.Name;
                p.PRODUCT_DESCRIPTION = pm.Description;
                p.PRODUCT_AVAILABLEQUANTITY = pm.AvailableQuantity;
                p.PRODUCT_MEMBERREDUCTION = pm.MemberReduction;
                p.PRODUCT_CAT = pm.CategoryId;
                p.PRODUCT_USERID = pm.UserId;
                p.PRODUCT_PRICE = pm.Price;

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

        public int DeleteProduct(ProductModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            PRODUCT p = db.PRODUCT.Find(pm.ProductId);

            if (p != null)
            {
                db.PRODUCT.Remove(p);
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

        public List<ProductModel> FindAllProducts()
        {
            List<ProductModel> productsList = new List<ProductModel>();
            IQueryable<PRODUCT> query = db.Set<PRODUCT>();

            foreach (PRODUCT p in query)
            {
                productsList.Add(cu.ConvertProductToProductModel(p));
            }

            return productsList;
        }
    }
}