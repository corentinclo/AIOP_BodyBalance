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
        Boolean CreateUser(UserModel um);
        UserModel FindUserById(String id);
        Boolean UpdateUser(UserModel um);
        Boolean DeleteUser(UserModel um);
        List<UserModel> FindAllUsers();
    }
}
