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
    public class CategoryServices : ICategoryServices
    {
        private Entities db = new Entities();
        private ConverterUtilities cu = new ConverterUtilities();
        private IProductServices pcs;

        public int CreateCategory(CategoryModel cm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CATEGORY c = db.CATEGORY.Create();

            c.CAT_ID = cm.CategoryId;
            c.CAT_NAME = cm.Name;
            c.CAT_DESCR = cm.Description;
            c.CAT_VALIDATIONDATE = cm.ValidationDate;
            c.CAT_PARENT = cm.ParentId;

            db.CATEGORY.Add(c);
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

        public CategoryModel FindCategoryWithId(string CategoryId)
        {
            CATEGORY c = db.CATEGORY.Find(CategoryId);

            return cu.ConvertCategoryToCategoryModel(c);
        }

        public int UpdateCategory(CategoryModel cm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CATEGORY c = db.CATEGORY.Find(cm.CategoryId);

            if (c != null)
            {
                c.CAT_ID = cm.CategoryId;
                c.CAT_NAME = cm.Name;
                c.CAT_DESCR = cm.Description;
                c.CAT_VALIDATIONDATE = cm.ValidationDate;
                c.CAT_PARENT = cm.ParentId;

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

        public int DeleteCategory(CategoryModel cm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CATEGORY c = db.CATEGORY.Find(cm.CategoryId);

            if (c != null)
            {
                db.CATEGORY.Remove(c);
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

        public List<CategoryModel> FindAllCategories()
        {
            List<CategoryModel> categoriesList = new List<CategoryModel>();
            IQueryable<CATEGORY> query = db.Set<CATEGORY>();

            foreach (CATEGORY c in query)
            {
                categoriesList.Add(cu.ConvertCategoryToCategoryModel(c));
            }

            return categoriesList;
        }

        public List<ProductModel> FindAllProductsOfCategory(string CategoryId)
        {
            List<ProductModel> productsList = new List<ProductModel>();
            IQueryable<PRODUCT> query = db.Set<PRODUCT>().Where(PRODUCT => PRODUCT.PRODUCT_CAT == CategoryId);

            foreach (PRODUCT p in query)
            {
                productsList.Add(cu.ConvertProductToProductModel(p));
            }

            return productsList;
        }

        public int AddProductToCategory(string CategoryId, ProductModel pm)
        {
            int result = DaoUtilities.NO_CHANGES;

            CATEGORY c = db.CATEGORY.Find(CategoryId);

            if (c != null)
            {
                PRODUCT p = db.PRODUCT.Find(pm.ProductId);
                if (p == null)
                {
                    pcs = new ProductServices();
                    int creationResult = pcs.CreateProduct(pm);
                    if (creationResult == DaoUtilities.SAVE_SUCCESSFUL)
                    {
                        p = db.PRODUCT.Find(pm.ProductId);
                        c.PRODUCT.Add(p);
                        p.CATEGORY = c;
                    }
                }
                else
                {
                    c.PRODUCT.Add(p);
                    p.CATEGORY = c;
                }
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
    }
}