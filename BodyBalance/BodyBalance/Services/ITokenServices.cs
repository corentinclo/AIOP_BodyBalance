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
        int CreateToken(TokenModel tm);
        TokenModel FindToken(String id, String token);
        int UpdateToken(TokenModel tm);
        int DeleteToken(TokenModel tm);

    }
}
