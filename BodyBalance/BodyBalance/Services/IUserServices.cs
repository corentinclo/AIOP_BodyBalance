using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="um"> A user model which contains all the information about the user</param>
        /// <returns></returns>
        int CreateUser(UserModel um);

        /// <summary>
        /// Find a user with his id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserModel FindUserById(string UserId);

        /// <summary>
        /// Find user by id and password
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        UserModel FindUserByIdAndPassword(string UserId, string pwd);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        int UpdateUser(UserModel um);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        int DeleteUser(UserModel um);

        /// <summary>
        /// Retrieves all the users
        /// </summary>
        /// <returns></returns>
        List<UserModel> FindAllUsers();

        /// <summary>
        /// Return true if this user is an admin
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        bool IsAdmin(UserModel um);

        /// <summary>
        /// Return true if this user is a contributor
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        bool IsContributor(UserModel um);

        /// <summary>
        /// Return true if this user is a manager
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        bool IsManager(UserModel um);

        /// <summary>
        /// Return true if this user is a member
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        bool IsMember(UserModel um);

        /// <summary>
        /// Find all the products of the user with the id in parameter
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<ProductModel> FindAllProductsOfUser(string UserId);

        /// <summary>
        /// Find all the notifications of the user with the id in parameter
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<NotificationModel> FindAllNotificationssOfUser(string UserId);

        /// <summary>
        /// Find all the purchases of the user with the id in parameter
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        List<PurchaseModel> FindAllPurchasesOfUser(string UserId);
    }
}
