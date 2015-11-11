using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IPriceServices
    {
        /// <summary>
        /// Create a price
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int CreatePrice(PriceModel pm);

        /// <summary>
        /// Find a price with its id
        /// </summary>
        /// <param name="PriceId"></param>
        /// <returns></returns>
        PriceModel FindPriceWithId(string PriceId);

        /// <summary>
        /// Update a price
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int UpdatePrice(PriceModel pm);

        /// <summary>
        /// Delete a price
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        int DeletePrice(PriceModel pm);

        /// <summary>
        /// Retrieve all the prices
        /// </summary>
        /// <returns></returns>
        List<PriceModel> FindAllPrices();

    }
}
