using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IProductServices
    {
        /// <summary>
        /// Create a product
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int CreateProduct(ProductModel pm);

        /// <summary>
        /// Find a product with its id
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        ProductModel FindProductWithId(string ProductId);

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int UpdateProduct(ProductModel pm);

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int DeleteProduct(ProductModel pm);

        /// <summary>
        /// Retrieve all the products
        /// </summary>
        /// <returns></returns>
        List<ProductModel> FindAllProducts();

    }
}
