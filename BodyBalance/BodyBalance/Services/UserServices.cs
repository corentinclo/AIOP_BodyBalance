using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Persistence;

namespace BodyBalance.Services
{
    public class UserServices
    {
        private DbBodyBalance db = new DbBodyBalance();
        
        public Boolean CreateUser(UserModel um)
        {
            USER1 u = db.USER1.Create();

            u.USER_ID = um.UserId;
            u.USER_PASSWORD = um.Password;
            u.USER_FIRSTNAME = um.FirstName;
            u.USER_LASTNAME = um.LastName;
            u.USER_ADR1 = um.Adress1;
            u.USER_ADR2 = um.Adress2;
            u.USER_PC = um.PC;
            u.USER_TOWN = um.Town;
            u.USER_PHONE = um.Phone;
            u.USER_MAIL = um.Mail;

            db.USER1.Add(u);
            int saveResult = db.SaveChanges();

            if (saveResult == 0)
                return false;

            return true;
        }

        public UserModel FindUserById(String id)
        {
            UserModel um = new UserModel();

            USER1 u = db.USER1.Find(id);
            um.UserId = u.USER_ID;
            um.Password = u.USER_PASSWORD;
            um.FirstName = u.USER_FIRSTNAME;
            um.LastName = u.USER_LASTNAME;
            um.Adress1 = u.USER_ADR1;
            um.Adress2 = u.USER_ADR2;
            um.PC = u.USER_PC;
            um.Town = u.USER_TOWN;
            um.Phone = u.USER_PHONE;
            um.Mail = u.USER_MAIL;

            return um;
        }

        public Boolean UpdateUser(UserModel um)
        {
            USER1 u = db.USER1.Find(um.UserId);

            if (u != null)
            {
                u.USER_PASSWORD = um.Password;
                u.USER_FIRSTNAME = um.FirstName;
                u.USER_LASTNAME = um.LastName;
                u.USER_ADR1 = um.Adress1;
                u.USER_ADR2 = um.Adress2;
                u.USER_PC = um.PC;
                u.USER_TOWN = um.Town;
                u.USER_PHONE = um.Phone;
                u.USER_MAIL = um.Mail;

                int saveResult = db.SaveChanges();
                if (saveResult == 0)
                    return false;

                return true;
            }
            return false;
        }

        public Boolean DeleteUser(UserModel um)
        {
            USER1 u = db.USER1.Find(um.UserId);

            if (u != null)
            {
                db.USER1.Remove(u);
                int saveResult = db.SaveChanges();

                if (saveResult == 0)
                    return false;

                return true;
            }
            return false;
        }
    }
}