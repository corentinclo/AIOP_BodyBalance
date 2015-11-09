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
        Boolean CreateUser(UserModel um);

        /// <summary>
        /// Find a user with his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel FindUserById(String id);

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        Boolean UpdateUser(UserModel um);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="um"></param>
        /// <returns></returns>
        Boolean DeleteUser(UserModel um);

        /// <summary>
        /// Retrieves all the users
        /// </summary>
        /// <returns></returns>
        List<UserModel> FindAllUsers();

        /// <summary>
        /// Find user by id and password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        UserModel FindUserByIdAndPassword(String id, String pwd);
    }
}
