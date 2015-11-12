using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface ICategoryServices
    {
        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int CreateCategory(CategoryModel cm);

        /// <summary>
        /// Find a category with its id
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        CategoryModel FindCategoryWithId(string CategoryId);

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int UpdateCategory(CategoryModel cm);

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        int DeleteCategory(CategoryModel cm);

        /// <summary>
        /// Retrieve all the categories
        /// </summary>
        /// <returns></returns>
        List<CategoryModel> FindAllCategories();

        /// <summary>
        /// Find all the products of the category with the id in parameter
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        List<ProductModel> FindAllProductsOfCategory(string CategoryId);
    }
}
