using BodyBalance.Models;
using BodyBalance.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BodyBalance.Services
{
    public class TokenServices
    {
        private DbBodyBalance db = new DbBodyBalance();

        public Boolean CreateToken(TokenModel tm)
        {
            TOKEN t = db.TOKEN.Create();

            t.USER_ID = tm.UserId;
            t.TOKEN1 = tm.Token;
            t.EXP_DATE = tm.ExpireDate;

            db.TOKEN.Add(t);
            int saveResult = db.SaveChanges();

            if (saveResult == 0)
                return false;

            return true;
        }

        public TokenModel FindToken(String id, String token)
        {
            TokenModel tm = new TokenModel();

            TOKEN t = db.TOKEN.Find(id, token);
            tm.UserId = t.USER_ID;
            tm.Token = t.TOKEN1;
            tm.ExpireDate = t.EXP_DATE;

            return tm;
        }

        public Boolean UpdateToken(TokenModel tm)
        {
            TOKEN t = db.TOKEN.Find(tm.UserId, tm.Token);

            if (t != null)
            {
                t.TOKEN1 = tm.Token;
                t.EXP_DATE = tm.ExpireDate;

                int saveResult = db.SaveChanges();
                if (saveResult == 0)
                    return false;

                return true;
            }
            return false;
        }

        public Boolean DeleteToken(TokenModel tm)
        {
            TOKEN t = db.TOKEN.Find(tm.UserId, tm.Token);

            if (t != null)
            {
                db.TOKEN.Remove(t);
                int saveResult = db.SaveChanges();

                if (saveResult == 0)
                    return false;

                return true;
            }
            return false;
        }
    }
}