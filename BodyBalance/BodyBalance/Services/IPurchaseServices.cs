using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IPurchaseServices
    {
        /// <summary>
        /// Create a purchase
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int CreatePurchase(PurchaseModel pm);

        /// <summary>
        /// Find a purchase with its id
        /// </summary>
        /// <param name="PurchaseId"></param>
        /// <returns></returns>
        PurchaseModel FindPurchaseWithId(string PurchaseId);

        /// <summary>
        /// Update a purchase
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int UpdatePurchase(PurchaseModel pm);

        /// <summary>
        /// Delete a purchase
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int DeletePurchase(PurchaseModel pm);

        /// <summary>
        /// Retrieve all the purchases
        /// </summary>
        /// <returns></returns>
        List<PurchaseModel> FindAllPurchases();

        /// <summary>
        /// Find all the products of the purchase with the id in parameter
        /// </summary>
        /// <param name="PurchaseId"></param>
        /// <returns></returns>
        List<ProductModel> FindAllProductsOfPurchase(string PurchaseId);
    }
}
