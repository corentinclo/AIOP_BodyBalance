using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IBasketServices
    {
        /// <summary>
        /// Create a line in the basket of a user
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        int CreateBasketLine(BasketModel bm);

        /// <summary>
        /// Find a basket line with the user and the product
        /// </summary>
        /// <param name="um"></param>
        /// <param name="pm"></param>
        /// <returns></returns>
        BasketModel FindBasketLineWithIds(string UserId, string ProductId);

        /// <summary>
        /// Update a line in the basket of a user
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        int UpdateBasketLine(BasketModel bm);

        /// <summary>
        /// Delete a line in the basket of a user
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        int DeleteBasketLine(BasketModel bm);
    }
}
