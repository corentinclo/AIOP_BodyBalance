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
    public class MemberServices : IMemberServices
    {
        private Entities db = new Entities();

        public int CreateMember(MemberModel mm)
        {
            int result = DaoUtilities.NO_CHANGES; 

            MEMBER m = db.MEMBER.Create();

            m.MEMBER_ID = mm.UserId;
            m.MEMBER_PAYFEEDATE = mm.PayDate;

            db.MEMBER.Add(m);
            try
            {
                int saveResult = db.SaveChanges();

                if (saveResult == 1)
                    result = DaoUtilities.SAVE_SUCCESSFUL;
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.UPDATE_EXCEPTION;
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.UNSUPPORTED_EXCEPTION;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.DISPOSED_EXCEPTION;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.GetBaseException().ToString());
                result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
            }

            return result;
        }

        public MemberModel FindMemberById(string MemberId)
        {
            MEMBER m = db.MEMBER.Find(MemberId);

            return ConvertMemberToMemberModel(m);
        }

        public int UpdateMember(MemberModel mm)
        {
            int result = DaoUtilities.NO_CHANGES;

            MEMBER m = db.MEMBER.Find(mm.UserId);

            if (m != null)
            {
                m.MEMBER_PAYFEEDATE = mm.PayDate;

                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult == 1)
                        result = DaoUtilities.SAVE_SUCCESSFUL;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }
            }
            return result;
        }

        public int DeleteMember(MemberModel mm)
        {
            int result = DaoUtilities.NO_CHANGES;

            MEMBER m = db.MEMBER.Find(mm.UserId);

            if (m != null)
            {
                db.MEMBER.Remove(m);
                try
                {
                    int saveResult = db.SaveChanges();

                    if (saveResult == 1)
                        result = DaoUtilities.SAVE_SUCCESSFUL;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_CONCURRENCY_EXCEPTION;
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UPDATE_EXCEPTION;
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.ENTITY_VALIDATION_EXCEPTION;
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.UNSUPPORTED_EXCEPTION;
                }
                catch (ObjectDisposedException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.DISPOSED_EXCEPTION;
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.GetBaseException().ToString());
                    result = DaoUtilities.INVALID_OPERATION_EXCEPTION;
                }
            }
            return result;
        }

        public List<MemberModel> FindAllMembers()
        {
            List<MemberModel> membersList = new List<MemberModel>();
            IQueryable<MEMBER> query = db.Set<MEMBER>();

            foreach (MEMBER m in query)
            {
                membersList.Add(ConvertMemberToMemberModel(m));
            }

            return membersList;
        }

        private MemberModel ConvertMemberToMemberModel(MEMBER m)
        {
            USER1 u = db.USER1.Find(m.MEMBER_ID);

            MemberModel mm = new MemberModel();

            if (m != null && u != null)
            {
                mm.UserId = u.USER_ID;
                mm.Password = u.USER_PASSWORD;
                mm.FirstName = u.USER_FIRSTNAME;
                mm.LastName = u.USER_LASTNAME;
                mm.Adress1 = u.USER_ADR1;
                mm.Adress2 = u.USER_ADR2;
                mm.PC = u.USER_PC;
                mm.Town = u.USER_TOWN;
                mm.Phone = u.USER_PHONE;
                mm.Mail = u.USER_MAIL;

                mm.PayDate = m.MEMBER_PAYFEEDATE;
            }
            else
                mm = null;

            return mm;
        }
    }
}