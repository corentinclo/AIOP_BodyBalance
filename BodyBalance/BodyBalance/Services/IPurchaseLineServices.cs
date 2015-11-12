using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IPurchaseLineServices
    {
        /// <summary>
        /// Create a purchaseLine
        /// </summary>
        /// <param name="plm"></param>
        /// <returns></returns>
        int CreatePurchaseLine(PurchaseLineModel plm);

        /// <summary>
        /// Find a purchaseLine with its PurchaseId and ProductId
        /// </summary>
        /// <param name="PurchaseId"></param>
        /// /// <param name="ProductId"></param>
        /// <returns></returns>
        PurchaseLineModel FindPurchaseLineWithIds(string PurchaseId, string ProductId);

        /// <summary>
        /// Update a purchase line
        /// </summary>
        /// <param name="plm"></param>
        /// <returns></returns>
        int UpdatePurchaseLine(PurchaseLineModel plm);

        /// <summary>
        /// Delete a purchaseLine
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int DeletePurchase(PurchaseLineModel plm);
    }
}
