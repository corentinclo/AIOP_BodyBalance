using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface ITokenServices
    {
        /// <summary>
        /// Create a token
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        int CreateToken(TokenModel tm);

        /// <summary>
        /// Find a token with its id and its token string
        /// </summary>
        /// <param name="TokenId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        TokenModel FindToken(String TokenId, String token);

        /// <summary>
        /// Update a token
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        int UpdateToken(TokenModel tm);

        /// <summary>
        /// Delete a token
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        int DeleteToken(TokenModel tm);

    }
}
