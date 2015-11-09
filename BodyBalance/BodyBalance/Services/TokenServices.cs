using BodyBalance.Models;
using BodyBalance.Persistence;
using BodyBalance.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace BodyBalance.Services
{
    public class TokenServices : ITokenServices
    {
        private Entities db = new Entities();

        public int CreateToken(TokenModel tm)
        {
            int result;

            TOKEN t = db.TOKEN.Create();

            t.USER_ID = tm.UserId;
            t.TOKEN1 = tm.Token;
            t.EXP_DATE = tm.ExpireDate;

            db.TOKEN.Add(t);
            try
            {
                int saveResult = db.SaveChanges();

                if (saveResult == 1)
                    result = DaoUtilities.SAVE_SUCCESSFUL;
            }
            catch (DbUpdateConcurrencyException e)
            {
                result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
            }
            catch (DbUpdateException e)
            {
                result = DaoUtilities.UPDATE_EXCEPTION;
            }
            catch (DbEntityValidationException e)
            {
                result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
            }
            catch (NotSupportedException e)
            {
                result = DaoUtilities.UNSUPPORTED_EXCEPTION;
            }
            catch (ObjectDisposedException e)
            {
                result = DaoUtilities.DISPOSED_EXCEPTION;
            }
            catch (InvalidOperationException e)
            {
                result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
            }

            result = DaoUtilities.NO_CHANGES;
            return result;
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

        public int UpdateToken(TokenModel tm)
        {
            int result = DaoUtilities.NO_CHANGES;

            TOKEN t = db.TOKEN.Find(tm.UserId, tm.Token);

            if (t != null)
            {
                t.TOKEN1 = tm.Token;
                t.EXP_DATE = tm.ExpireDate;

                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult == 1)
                        result = DaoUtilities.SAVE_SUCCESSFUL;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }

                result = DaoUtilities.NO_CHANGES;
            }
            return result;
        }

        public int DeleteToken(TokenModel tm)
        {
            int result = DaoUtilities.NO_CHANGES;

            TOKEN t = db.TOKEN.Find(tm.UserId, tm.Token);

            if (t != null)
            {
                db.TOKEN.Remove(t);
                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult == 1)
                        result = DaoUtilities.SAVE_SUCCESSFUL;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }

                result = DaoUtilities.NO_CHANGES;
            }
            return result;
        }
    }
}