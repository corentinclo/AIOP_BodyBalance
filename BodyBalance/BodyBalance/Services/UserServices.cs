using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BodyBalance.Models;
using BodyBalance.Persistence;
using System.Security.Cryptography;
using System.Text;

namespace BodyBalance.Services
{
    public class UserServices : IUserServices
    {
        private Entities db = new Entities();
        
        public Boolean CreateUser(UserModel um)
        {
            USER1 u = db.USER1.Create();

            u.USER_ID = um.UserId;
            u.USER_PASSWORD = hashSHA512(um.Password);
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
            USER1 u = db.USER1.Find(id);

            return ConvertUserToUserModel(u);
        }

        public UserModel FindUserByIdAndPassword(String id, String pwd)
        {
            pwd = hashSHA512(pwd);
            USER1 u = ((USER1) db.USER1.Where(USER1 => USER1.USER_ID == id && USER1.USER_PASSWORD == pwd).FirstOrDefault());

            return ConvertUserToUserModel(u);
        }

        public Boolean UpdateUser(UserModel um)
        {
            USER1 u = db.USER1.Find(um.UserId);

            if (u != null)
            {
                u.USER_PASSWORD = hashSHA512(um.Password);
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

        public List<UserModel> FindAllUsers()
        {
            List<UserModel> usersList = new List<UserModel>();
            IQueryable<USER1> query = db.Set<USER1>();

            foreach(USER1 u in query)
            {
                usersList.Add(ConvertUserToUserModel(u));
            }

            return usersList;
        }

        public Boolean IsAdmin(UserModel um)
        {
            if (db.ADMIN.Find(um.UserId) != null)
                return true;

            return false;
        }

        public Boolean IsContributor(UserModel um)
        {
            if (db.CONTRIBUTOR.Find(um.UserId) != null)
                return true;

            return false;
        }

        public Boolean IsManager(UserModel um)
        {
            if (db.MANAGER.Find(um.UserId) != null)
                return true;

            return false;
        }

        public Boolean IsMember(UserModel um)
        {
            if (db.MEMBER.Find(um.UserId) != null)
                return true;

            return false;
        }

        private UserModel ConvertUserToUserModel(USER1 u)
        {
            UserModel um = new UserModel();

            if (u != null)
            {
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
            }
            else
                um = null;

            return um;
        }

        private string hashSHA512(string unhashedValue)
        {
            SHA512 shaM = new SHA512Managed();
            byte[] hash = shaM.ComputeHash(Encoding.ASCII.GetBytes(unhashedValue));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}