﻿using BodyBalance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyBalance.Services
{
    interface IAdminServices
    {
        /// <summary>
        /// Create an admin
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int CreateAdmin(AdminModel am);

        /// <summary>
        /// Find an admin with his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AdminModel FindAdminById(string id);

        /// <summary>
        /// Delete an admin
        /// </summary>
        /// <param name="am"></param>
        /// <returns></returns>
        int DeleteAdmin(AdminModel am);

        /// <summary>
        /// Retrieve all the admins
        /// </summary>
        /// <returns></returns>
        List<AdminModel> FindAllAdmins();
    }
}